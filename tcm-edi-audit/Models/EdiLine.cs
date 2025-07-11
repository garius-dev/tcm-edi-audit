using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcm_edi_audit.Extensions;
using tcm_edi_audit.Models.Settings;

namespace tcm_edi_audit.Models
{
    public class EdiLine
    {
        public string LineCode { get; set; }
        public int Id { get; set; }

        public bool HasErrors { get; set; }
        public bool HasFatalErrors { get; set; } = false;
        public string Error { get; set; }
        public List<EdiColumn> Columns { get; set; } = new List<EdiColumn>();


        private static List<EdiColumn> DecodeColumns(string lineText, List<EdiFieldValidationSettings> ediFieldValidationSettings)
        {
            List<EdiColumn> columns = new List<EdiColumn>();

            if (lineText.Length < 680 || lineText.Length > 681)
            {
                columns.Add(new EdiColumn()
                {
                    Id = 0,
                    Content = string.Empty,
                    IsValidated = false,
                    Error = "O tamanho da linha está fora dos limites permitidos (680 a 681 caracteres)",
                });

                return columns;
            }

            foreach (var columnRule in ediFieldValidationSettings)
            {
                if (columnRule.TextStartPosition < 0 ||
                    columnRule.TextStartPosition + columnRule.TextLength > lineText.Length)
                {
                    columns.Add(new EdiColumn()
                    {
                        Id = columnRule.ColumnId,
                        Content = string.Empty,
                        IsValidated = false,
                        Error = "A posição da coluna está incorreta ou excede o limite."
                    });
                }
                else
                {
                    var columnValue = lineText.Substring(columnRule.TextStartPosition, columnRule.TextLength)?.Trim();

                    var validationResult = columnRule.ValidateText(columnValue);

                    columns.Add(new EdiColumn()
                    {
                        Id = columnRule.ColumnId,
                        Content = columnValue,
                        IsValidated = validationResult.Success,
                        Error = validationResult.Success ? null : string.Join(", ", validationResult.errors)
                    });
                }
            }

            return columns;
        }

        

        public static List<EdiLine> DecodeLines(string[] fileLines, AppSettings _settings)
        {
            List<EdiLine> ediLines = new List<EdiLine>();
            int lineNumber = 0;

            if (!fileLines.IsNullOrEmpty())
            {
                foreach (var line in fileLines)
                {
                    var lineCode = line.Substring(0, 3).Trim();
                    if (!string.IsNullOrEmpty(lineCode) && lineCode.Length == 3)
                    {
                        var lineConfig = _settings.EdiFieldValidation.GetListByCode(lineCode);
                        if (lineConfig != null)
                        {
                            var decodedColumns = DecodeColumns(line, lineConfig);

                            EdiLine ediLine = new EdiLine
                            {
                                Id = lineNumber,
                                LineCode = lineCode,
                                Columns = decodedColumns,
                                HasErrors = decodedColumns.Any(c => !c.IsValidated)
                            };

                            ediLines.Add(ediLine);
                        }
                        else
                        {
                            EdiLine errorLine = new EdiLine()
                            {
                                Id = lineNumber,
                                HasErrors = true,
                                HasFatalErrors = true,
                                LineCode = lineCode,
                                Error = $"Configuração do código de linha '{lineCode}' não encontrado."
                            };

                            ediLines.Add(errorLine);
                        }
                    }
                    else
                    {
                        EdiLine errorLine = new EdiLine()
                        {
                            Id = lineNumber,
                            HasErrors = true,
                            HasFatalErrors = true,
                            LineCode = lineCode,
                            Error = "Código da linha Inválido"
                        };

                        ediLines.Add(errorLine);
                    }

                    lineNumber++;
                }

                foreach (var ediLine in ediLines)
                {
                    if (ediLine.LineCode == "322")
                    {
                        if (ediLines.Get329Peer(ediLine.Columns[3].Content) == null)
                        {
                            ediLine.HasFatalErrors = true;
                            ediLine.HasErrors = true;
                            ediLine.Columns[3].IsValidated = true;
                            ediLine.Columns[3].Error = $"Não foi encontrado o código de conhecimento '{ediLine.Columns[3].Content}' na linha 329";
                        }
                    }
                }
            }
            else
            {
                return null;
            }

            return ediLines;
        }
    }

    public class EdiColumn
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsValidated { get; set; } = true;
        public string Error { get; set; }

    }
}
