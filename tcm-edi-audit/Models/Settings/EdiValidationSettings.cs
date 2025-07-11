using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace tcm_edi_audit.Models.Settings
{
    public static class EdiFieldValidationExtensions
    {
        public static List<EdiFieldValidationSettings> GetListByCode(this List<EdiFieldValidationSettings> list, string code)
        {
            return list.Where(w => w.LineCode == code).ToList();
        }

        public static void ValidatePeers(List<EdiLine> ediLines, ExcelRecordGroup excelRecordGroup)
        {
            var lines322 = ediLines.Where(w => w.LineCode == "322").ToList();
            var lines329 = ediLines.Where(w => w.LineCode == "329").ToList();

            foreach (var line in lines322)
            {
                var knowledgeNumber = line.Columns[3].Content;

                var foundPeer = lines329.Any(a => a.Columns.Any(b => b.Content.StartsWith(knowledgeNumber)));
                if (!foundPeer)
                {
                    line.HasErrors = true;
                    line.Columns[3].IsValidated = false;
                    line.Columns[3].Error = $"Não foi encontrado uma linha 329 correspondente ({knowledgeNumber})";
                }
            }

        }

        public static void ValidateVehicle(List<EdiColumn> columns, AppSettings settings, ExcelRecordGroup excelRecordGroup)
        {
            int idx = 1;

            if (columns.Count > idx)
            {
                string vehicleCodeText = columns[idx].Content;

                var foundVehicle = settings.Vehicles.FirstOrDefault(b => b.Code == vehicleCodeText);

                if (foundVehicle == null)
                {
                    columns[idx].IsValidated = false;
                    columns[idx].Error = "Código de veículo inválido";
                }
            }
        }

        public static void ValidateCollectType(List<EdiColumn> columns, AppSettings settings, ExcelRecordGroup excelRecordGroup)
        {
            int idx = 14;

            if (columns.Count > idx)
            {
                string collectCodeText = columns[idx].Content;
                var foundCollectType = settings.Collections.FirstOrDefault(b => b.Code == collectCodeText);
                if (foundCollectType == null)
                {
                    columns[idx].IsValidated = false;
                    columns[idx].Error = "Código de veículo inválido";
                }
            }
        }

        public static void ValidateCollectRequestCode(List<EdiColumn> columns, AppSettings settings, ExcelRecordGroup excelRecordGroup)
        {
            int idx = 7;

            var requestNumbers = excelRecordGroup.Records.GroupBy(g => g.RequestCode).Select(s => s.Key).ToList();

            if (columns.Count > idx)
            {
                string collectRequestCode = columns[idx].Content;

                bool isBinaryText = collectRequestCode.All(c => c == '0' || c == '1');

                if (collectRequestCode != requestNumbers.First() || (isBinaryText && collectRequestCode != "000001"))
                {
                    columns[idx].IsValidated = false;
                    columns[idx].Error = "Código de solicitação de coleta inválido (qtd. de zeros)";
                }

            }

        }

        public static void ValidateTotalAmount(List<EdiColumn> columns, string textTotalAmount, ExcelRecordGroup excelRecordGroup)
        {
            int idx_a = 2;

            if (!columns[idx_a].Content.EndsWith(textTotalAmount))
            {
                columns[idx_a].IsValidated = false;
                columns[idx_a].Error = $"Valor total do documento não bate com a planilha, valor esperado {textTotalAmount}, obtido: {columns[idx_a].Content}";
            }
        }

        public static void ValidateBranch(List<EdiColumn> columns322, List<EdiColumn> columns329, AppSettings settings, ExcelRecordGroup excelRecordGroup)
        {
            int id_BranchCode322 = 2;
            int id_BranchCode329 = 13;
            int id_BranchName322 = 1;
            int id_ND329 = 12;

            if (columns322.Count > id_BranchCode322 && columns329.Count > id_BranchCode329)
            {
                string branchName = columns322[id_BranchName322].Content;
                bool branchCode322ParseResult = int.TryParse(columns322[id_BranchCode322].Content, out int branchCode322); //columns322[id_BranchCode322].Content;
                bool branchCode329ParseResult = int.TryParse(columns329[id_BranchCode329].Content, out int branchCode329);

                if (!branchCode322ParseResult && columns322[id_BranchCode322].Content == "ND")
                {
                    string ndContent = columns329[id_ND329].Content;
                    if (!ndContent.EndsWith("ND"))
                    {
                        columns329[id_ND329].IsValidated = false;
                        columns329[id_ND329].Error = "Código de ND inválido";
                    }
                    return;
                }

                var foundBranch = settings.Branches
                    .FirstOrDefault(x => (x.Name.Length >= 10 ? x.Name.Substring(0, 10) : x.Name)?.ToUpper().Trim() == branchName);

                if (foundBranch != null)
                {
                    if (!branchCode322ParseResult ||
                        !branchCode329ParseResult ||
                        branchCode322 % 2 == 0 ||
                        branchCode322 != branchCode329 ||
                        branchCode322 != foundBranch.OddCode ||
                        branchCode329 != foundBranch.OddCode)
                    {
                        columns322[id_BranchName322].IsValidated = false;
                        columns322[id_BranchName322].Error = "Código de filial inválido";

                        columns329[id_BranchCode329].IsValidated = false;
                        columns329[id_BranchCode329].Error = "Código de filial inválido";
                    }
                }
                else
                {
                    columns322[id_BranchName322].IsValidated = false;
                    columns322[id_BranchName322].Error = "Nome de filial inválido";
                }

            }
        }

    }

    public class EdiFieldValidationSettings
    {
        [DisplayName("#")]
        public int ColumnId { get; set; }
        public string LineCode { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public int TextStartPosition { get; set; }
        public int TextLength { get; set; }
        public int Dec { get; set; }


        public int ExpectedCharCount { get; set; } = 0;
        public string AllowedText { get; set; } = string.Empty;
        public string AllowedFormat { get; set; } = string.Empty;
        public string AllowedRegex { get; set; } = string.Empty;



        public (bool Success, List<string> errors) ValidateText(string text)
        {
            (bool Success, List<string> errors) result = (true, new List<string>());
            result.Success = true;
            result.errors = new List<string>();

            this.FieldName = this.FieldName.Trim();
            this.FieldType = this.FieldType.Trim();

            if (this.FieldType?.Trim() == "D")
            {
                string dateFormat = this.AllowedFormat;
                bool isValidDate = DateTime.TryParseExact(
                    text,
                    dateFormat,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime dataConvertida
                );

                if (!isValidDate)
                {
                    result.Success = false;
                    result.errors.Add($"Field '{this.FieldName}' should be a date in format '{dateFormat}', but got '{text}'.");
                }
            }


            if (this.FieldType?.Trim() == "N")
            {
                if (!string.IsNullOrEmpty(text))
                {
                    if (!decimal.TryParse(text, out _))
                    {
                        result.Success = false;
                        result.errors.Add($"Field '{this.FieldName}' should be a number, but got '{text}'.");
                    }
                }
            }

            if (this.ExpectedCharCount > 0 && text.Length != this.ExpectedCharCount)
            {
                result.Success = false;
                result.errors.Add($"Expected character count for '{this.FieldName}' is {this.ExpectedCharCount}, but got {text.Length}.");
            }



            if (this.FieldType?.Trim() == "C")
            {
                if (this.AllowedRegex.Length > 0)
                {
                    var regex = new Regex($@"{this.AllowedRegex}");
                    var match = regex.Match(text);
                    if (!match.Success)
                    {
                        result.Success = false;
                        result.errors.Add($"Field '{this.FieldName}' should match the regex '{this.AllowedRegex}', but got '{text}'.");
                    }
                }
            }


            if (this.AllowedText?.Length > 0)
            {
                var allowedTexts = this.AllowedText.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                                   .Select(s => s.Trim())
                                                   .ToArray();

                if (!allowedTexts.Contains(text))
                {
                    result.Success = false;
                    result.errors.Add($"Field '{this.FieldName}' should be one of the allowed values: {string.Join(", ", allowedTexts)}, but got '{text}'.");
                }
            }

            return result;
        }
    }
}
