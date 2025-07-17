using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using tcm_edi_audit_core.Extensions;
using tcm_edi_audit_core.Models.DTOs;
using tcm_edi_audit_core.Models.EDI;
using tcm_edi_audit_core.Models.EDI.Settings;
using tcm_edi_audit_core.Models.Settings;

namespace tcm_edi_audit_core.Services
{
    public class EdiValidatorService
    {
        private readonly AppSettings _settings;

        public EdiValidatorService(AppSettings settings)
        {
            _settings = settings;
        }


        // Adicionado o parâmetro opcional 'tryFixIt' para habilitar a autocorreção.
        public EdiValidationResult Validate(EdiParseResult ediParseResult, List<ExcelEntry> excelData, string invoiceNumber, bool tryFixIt = false)
        {
            var result = new EdiValidationResult();

            bool parseValidationResult = ValidateParsedLines(ediParseResult, result);
            //

            if (parseValidationResult)
            {
                ValidateVehicleCodes(ediParseResult.Lines, result);
                ValidateCollectTypeCodes(ediParseResult.Lines, result);
                ValidateBranchInformation(ediParseResult.Lines, result, tryFixIt);
                ValidateCollectRequestCode(ediParseResult.Lines, excelData, invoiceNumber, result, tryFixIt);
                ValidateInvoiceTotalRevenue(ediParseResult.Lines, excelData, invoiceNumber, result, tryFixIt);
            }            

            result.EdiLines = ediParseResult.Lines;

            if (result.Errors.Count > 0)
            {
                result.Status = EdiValidationStatus.Error;
                result.StatusIcon = Properties.Resources.circle_red_16_16;
            }
            else if (result.Warnings.Count > 0)
            {
                result.Status = EdiValidationStatus.Warning;
                result.StatusIcon = Properties.Resources.circle_yellow_16_16;
            }
            else
            {
                result.Status = EdiValidationStatus.Success;
                result.StatusIcon = Properties.Resources.circle_green_16_16;
            }

            return result;
        }

        private bool ValidateParsedLines(EdiParseResult ediParseResult, EdiValidationResult result)
        {
            if (!ediParseResult.Success)
            {
                foreach (var ediLine in ediParseResult.Lines)
                {
                    if (!ediLine.Success)
                    {
                        if (!ediLine.Columns.IsNullOrEmpty())
                        {
                            foreach (var ediColumn in ediLine.Columns)
                            {
                                if (!ediColumn.Success)
                                {
                                    result.Errors.Add($"Linha {ediLine.Id} ({ediLine.Code}), Coluna {ediColumn.Id} ({ediColumn.FieldDefinitionRule?.FieldName ?? "NA"}): {ediColumn.ErrorMessage}");
                                }
                            }
                        }
                        else
                        {
                            result.Errors.Add($"Linha {ediLine.Id} ({(string.IsNullOrEmpty(ediLine.Code) ? "NA" : ediLine.Code)}): {ediLine.ErrorMessage}");
                        }

                    }
                }

                return false;
            }

            return true;
        }

        private void ValidateVehicleCodes(List<EdiLine> lines, EdiValidationResult result)
        {
            var validVehicleCodes = _settings.Vehicles.Select(v => v.Code).ToHashSet();

            foreach (var line in lines.Where(l => l.Code == "329"))
            {
                var column = line.Columns.ElementAtOrDefault(1);
                if (column == null || !validVehicleCodes.Contains(column.Value))
                {
                    result.Errors.Add($"Linha {line.Id} (329): Código de veículo '{column?.Value}' inválido ou não configurado.");
                }
            }
        }

        private void ValidateCollectTypeCodes(List<EdiLine> lines, EdiValidationResult result)
        {
            var validCollectTypes = _settings.CollectTypes.Select(c => c.Code).ToHashSet();

            foreach (var line in lines.Where(l => l.Code == "329"))
            {
                var column = line.Columns.ElementAtOrDefault(14);
                if (column == null || !validCollectTypes.Contains(column.Value))
                {
                    result.Errors.Add($"Linha {line.Id} (329): Tipo de coleta '{column?.Value}' inválido ou não configurado.");
                }
            }
        }

        // Adicionado o parâmetro 'tryFixIt' para a lógica de correção.
        private void ValidateBranchInformation(List<EdiLine> lines, EdiValidationResult result, bool tryFixIt)
        {
            var lines322 = lines.Where(l => l.Code == "322").ToList();
            var lines329 = lines.Where(l => l.Code == "329").ToList();

            for (int i = 0; i < lines322.Count; i++)
            {
                var line322 = lines322[i];
                var line329 = lines329.ElementAtOrDefault(i);
                if (line329 == null) continue;

                var branchNameRaw = line322.Columns.ElementAtOrDefault(1)?.Value?.ToUpper() ?? "";
                var branchCode322Col = line322.Columns.ElementAtOrDefault(2);
                var branchCode329Col = line329.Columns.ElementAtOrDefault(13);
                var ndIdentifierCol = line329.Columns.ElementAtOrDefault(12);

                var matchedBranch = _settings.Branches.FirstOrDefault(b =>
                {
                    var normalizedName = b.Name.Length > 10 ? b.Name.Substring(0, 10) : b.Name;
                    return branchNameRaw.StartsWith(normalizedName.ToUpper());
                });

                if (matchedBranch == null)
                {
                    result.Errors.Add($"Linha {line322.Id} (322): Nome da filial '{branchNameRaw}' não encontrado nas configurações.");
                    continue;
                }

                if (branchCode322Col?.Value == "ND")
                {
                    if (!ndIdentifierCol.Value.EndsWith("ND"))
                    {
                        // Se 'tryFixIt' for verdadeiro, corrige o valor em vez de registrar um erro.
                        if (tryFixIt)
                        {
                            var originalValue = ndIdentifierCol.Value;
                            ndIdentifierCol.Value += "ND";
                            result.Warnings.Add($"Linha {line329.Id} (329): Identificador da coleta '{originalValue}' corrigido para '{ndIdentifierCol.Value}'.");
                        }
                        else
                        {
                            result.Errors.Add($"Linha {line329.Id} (329): Identificador da coleta '{ndIdentifierCol.Value}' deveria terminar com 'ND'.");
                        }
                    }
                    continue;
                }

                var branchExpectedPrefix = (matchedBranch.Name.Length > 10 ? matchedBranch.Name.Substring(0, 10) : matchedBranch.Name).ToUpper();
                if (!branchNameRaw.StartsWith(branchExpectedPrefix))
                {
                    result.Errors.Add($"Linha {line322.Id} (322): Nome da filial '{branchNameRaw}' não corresponde à configuração '{branchExpectedPrefix}'.");
                }

                if (branchCode322Col?.Value != branchCode329Col?.Value)
                {
                    // Se 'tryFixIt' for verdadeiro, corrige o valor em vez de registrar um erro.
                    if (tryFixIt)
                    {
                        var originalValue = branchCode329Col.Value;
                        branchCode329Col.Value = branchCode322Col.Value;
                        result.Warnings.Add($"Linha {line329.Id} (329): Código da filial corrigido de '{originalValue}' para '{branchCode329Col.Value}' para corresponder à linha 322.");
                    }
                    else
                    {
                        result.Errors.Add($"Linhas {line322.Id}/{line329.Id}: Códigos da filial não coincidem (322: {branchCode322Col.Value}, 329: {branchCode329Col.Value}).");
                    }
                }

                if (!IsOddAndMatches(branchCode322Col?.Value, matchedBranch.OddCode))
                {
                    // Se 'tryFixIt' for verdadeiro, corrige o valor em vez de registrar um erro.
                    if (tryFixIt)
                    {
                        var originalValue = branchCode322Col.Value;
                        branchCode322Col.Value = matchedBranch.OddCode.ToString();
                        result.Warnings.Add($"Linha {line322.Id} (322): Código de filial '{originalValue}' corrigido para o código ímpar esperado '{branchCode322Col.Value}'.");

                        if (branchCode329Col != null)
                        {
                            branchCode329Col.Value = branchCode322Col.Value;
                            result.Warnings.Add($"Linha {line329.Id} (329): Código da filial atualizado para consistência após correção na linha 322.");
                        }
                    }
                    else
                    {
                        result.Errors.Add($"Linha {line322.Id} (322): Código de filial '{branchCode322Col.Value}' deve ser ímpar e igual a '{matchedBranch.OddCode}'.");
                    }
                }
            }
        }

        private bool IsOddAndMatches(string code, int expected)
        {
            return int.TryParse(code, out int number) && number % 2 == 1 && number == expected;
        }

        // Adicionado o parâmetro 'tryFixIt' para a lógica de correção.
        private void ValidateCollectRequestCode(List<EdiLine> ediLines, List<ExcelEntry> excelData, string invoiceNumber, EdiValidationResult result, bool tryFixIt)
        {
            var lines329 = ediLines.Where(l => l.Code == "329").ToList();
            if (!lines329.Any())
                return;

            var excelEntries = excelData
                .Where(e => e.Invoice == invoiceNumber)
                .ToList();

            if (!excelEntries.Any())
            {
                result.Errors.Add($"Fatura {invoiceNumber}: Não encontrada na planilha de protocolo.");
                return;
            }

            var expectedRequestCode = excelEntries
                .Select(e => e.RequestCode?.Trim())
                .Distinct()
                .SingleOrDefault();

            if (string.IsNullOrEmpty(expectedRequestCode))
            {
                result.Errors.Add($"Fatura {invoiceNumber}: Código de solicitação ausente ou múltiplo na planilha.");
                return;
            }

            foreach (var line in lines329)
            {
                var column = line.Columns.ElementAtOrDefault(7);
                if (column == null || string.IsNullOrEmpty(column.Value))
                {
                    result.Errors.Add($"Linha {line.Id} (329): Código de solicitação ausente.");
                    continue;
                }

                if (column.Value != expectedRequestCode)
                {
                    // Se 'tryFixIt' for verdadeiro, corrige o valor em vez de registrar um erro.
                    if (tryFixIt)
                    {
                        var originalValue = column.Value;
                        column.Value = expectedRequestCode;
                        result.Warnings.Add($"Linha {line.Id} (329): Código de solicitação '{originalValue}' corrigido para '{expectedRequestCode}' (baseado na planilha).");
                    }
                    else
                    {
                        result.Errors.Add($"Linha {line.Id} (329): Código de solicitação '{column.Value}' diferente do esperado '{expectedRequestCode}' para a fatura {invoiceNumber}.");
                    }

                    continue;
                }

                bool isOnlyZerosAndOnes = column.Value.All(c => c == '0' || c == '1');

                if (isOnlyZerosAndOnes)
                {
                    if (column.Value != "000001")
                    {
                        // Se 'tryFixIt' for verdadeiro, corrige o valor em vez de registrar um erro.
                        if (tryFixIt)
                        {
                            var originalValue = column.Value;
                            column.Value = "000001";
                            result.Warnings.Add($"Linha {line.Id} (329): Valor de solicitação '{originalValue}' corrigido para '000001'.");
                        }
                        else
                        {
                            result.Errors.Add($"Linha {line.Id} (329): Valor de solicitação '{column.Value}' inválido. Deve ser '000001' se composto por 0 e 1.");
                        }
                    }
                }
                else
                {

                }
            }
        }

        // Adicionado o parâmetro 'tryFixIt' para a lógica de correção.
        private void ValidateInvoiceTotalRevenue(List<EdiLine> ediLines, List<ExcelEntry> excelData, string invoiceNumber, EdiValidationResult result, bool tryFixIt)
        {
            var totalFromExcel = excelData
                .Where(r => r.Invoice == invoiceNumber)
                .Sum(r => TruncateToTwoDecimals(r.TotalRevenue));

            var totalInCents = (long)(totalFromExcel * 100);

            var registro323 = ediLines.FirstOrDefault(l => l.Code == "323");
            if (registro323 == null)
            {
                result.Errors.Add($"Fatura {invoiceNumber}: Linha 323 não encontrada no EDI.");
                return;
            }

            var ediValueCol = registro323.Columns.ElementAtOrDefault(2);
            if (ediValueCol == null || string.IsNullOrEmpty(ediValueCol.Value))
            {
                result.Errors.Add($"Linha {registro323.Id} (323): Valor da receita total (coluna 2) está ausente.");
                return;
            }

            if (!long.TryParse(ediValueCol.Value, out long ediValue))
            {
                result.Errors.Add($"Linha {registro323.Id} (323): Valor '{ediValueCol.Value}' inválido.");
                return;
            }

            var diference = Math.Abs(ediValue - totalInCents);

            if (ediValue != totalInCents && diference > 1)
            {
                // Se 'tryFixIt' for verdadeiro, corrige o valor em vez de registrar um erro.
                if (tryFixIt)
                {
                    var originalValue = ediValueCol.Value;
                    ediValueCol.Value = totalInCents.ToString("D15");
                    result.Warnings.Add($"Fatura {invoiceNumber}: Soma da receita total na planilha é {totalFromExcel:0.00}. Valor no EDI corrigido de '{originalValue}' para '{ediValueCol.Value}'.");
                }
                else
                {
                    result.Errors.Add(
                        $"Fatura {invoiceNumber}: Soma da receita total na planilha é {totalFromExcel:0.00} (esperado: {totalInCents:D15}), mas o EDI apresenta '{ediValueCol.Value}'.");
                }
            }
        }

        private decimal TruncateToTwoDecimals(decimal value)
        {
            return Math.Truncate(value * 100) / 100;
        }
    }
}
