using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tcm_edi_audit_core.Extensions;
using tcm_edi_audit_core.Models.EDI;
using tcm_edi_audit_core.Models.EDI.Settings;
using tcm_edi_audit_core.Models.Settings;
using tcm_edi_audit_core.Settings.Services;
using static System.Net.Mime.MediaTypeNames;

namespace tcm_edi_audit_core.Services
{
    public class EdiParserService
    {
        private readonly AppSettings _settings;

        public EdiParserService(AppSettings settings)
        {
            _settings = settings;
        }

        public EdiParseResult ParseFile(string[] lines)
        {
            var result = new EdiParseResult();

            for (int i = 0; i < lines.Length; i++)
            {
                var ediLine = new EdiLine { Id = i };

                if (lines[i].Length < 3)
                {
                    ediLine.ErrorMessage = "Código da linha ausente ou inválido (menos de 3 caracteres).";
                    result.Errors.Add($"Linha {i}: {ediLine.ErrorMessage}");
                    result.Lines.Add(ediLine);
                    continue;
                }

                var lineCode = lines[i].Substring(0, 3)?.Trim();
                var lineCodeConfig = _settings.GetCodeDefinition(lineCode);
                var fieldConfigs = _settings.GetFieldDefinition(lineCode);

                if (lineCodeConfig == null)
                {
                    ediLine.Code = lineCode;
                    ediLine.ErrorMessage = $"Código '{lineCode}' não possui configuração definida.";
                    result.Errors.Add($"Linha {i}: {ediLine.ErrorMessage}");
                    result.Lines.Add(ediLine);
                    continue;
                }

                if (fieldConfigs == null || !fieldConfigs.Any())
                {
                    ediLine.Code = lineCode;
                    ediLine.ErrorMessage = $"Configuração de colunas não encontrada para código '{lineCode}'.";
                    result.Errors.Add($"Linha {i}: {ediLine.ErrorMessage}");
                    result.Lines.Add(ediLine);
                    continue;
                }

                if (lines[i].Length < lineCodeConfig.MinLen || lines[i].Length > lineCodeConfig.MaxLen)
                {
                    ediLine.Code = lineCode;
                    ediLine.ErrorMessage = $"Tamanho da linha fora do intervalo esperado para código '{lineCode}' (esperado {lineCodeConfig.MaxLen}, obtido {lines[i].Length}).";
                    result.Errors.Add($"Linha {i}: {ediLine.ErrorMessage}");
                    result.Lines.Add(ediLine);
                    continue;
                }

                ediLine.Code = lineCode;

                for (int j = 0; j < fieldConfigs.Count; j++)
                {
                    var field = fieldConfigs[j];
                    var column = new EdiColumn { Id = j };

                    if (!lines[i].ValidateStringBounds(field.TextStartPosition, field.TextLength))
                    {
                        column.ErrorMessage = $"Campo '{field.FieldName}' (posição {field.TextStartPosition}, tamanho {field.TextLength}) está fora dos limites da linha.";
                        result.Errors.Add($"Linha {i}, Coluna {j}: {column.ErrorMessage}");
                        ediLine.Columns.Add(column);
                        continue;
                    }

                    column.Value = lines[i].Substring(field.TextStartPosition, field.TextLength)?.Trim();

                    if (!ValidateField(column, field, out var error))
                    {
                        column.ErrorMessage = error;
                        result.Errors.Add($"Linha {i}, Coluna {j}: {error}");
                        ediLine.Columns.Add(column);
                        continue;
                    }

                    column.FieldDefinitionRule = field;
                    column.Success = true;
                    ediLine.LineLenght = lineCodeConfig.MinLen;
                    ediLine.Columns.Add(column);
                }

                ediLine.Success = ediLine.Columns.All(c => c.Success);
                result.Lines.Add(ediLine);
            }

            return result;
        }

        private bool ValidateField(EdiColumn column, EdiFieldDefinitionSettings field, out string error)
        {
            error = string.Empty;

            if (field.FieldType?.Trim() == "D")
            {
                if (!DateTime.TryParseExact(column.Value, field.AllowedFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    error = $"Campo '{field.FieldName}' deve conter uma data válida no formato '{field.AllowedFormat}'.";
                    return false;
                }
            }

            if (field.FieldType?.Trim() == "N")
            {
                if (!string.IsNullOrEmpty(column.Value) && !decimal.TryParse(column.Value, out _))
                {
                    error = $"Campo '{field.FieldName}' deve conter um número válido.";
                    return false;
                }
            }

            if (field.FieldType?.Trim() == "C" && !string.IsNullOrEmpty(field.AllowedRegex))
            {
                if (!Regex.IsMatch(column.Value, field.AllowedRegex))
                {
                    error = $"Campo '{field.FieldName}' deve respeitar o padrão '{field.AllowedRegex}'.";
                    return false;
                }
            }

            if (field.ExpectedCharCount > 0 && column.Value.Length != field.ExpectedCharCount)
            {
                error = $"Campo '{field.FieldName}' deve conter exatamente {field.ExpectedCharCount} caracteres.";
                return false;
            }

            if (!string.IsNullOrEmpty(field.AllowedText))
            {
                var allowed = field.AllowedText.Split(';').Select(s => s.Trim());
                if (!allowed.Contains(column.Value))
                {
                    error = $"Campo '{field.FieldName}' deve conter um dos seguintes valores permitidos: {string.Join(", ", allowed)}.";
                    return false;
                }
            }

            return true;
        }
    }
}
