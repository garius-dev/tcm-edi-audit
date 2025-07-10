using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tcm_edi_audit.Models;
using tcm_edi_audit.Models.Settings;

namespace tcm_edi_audit.Services
{
    public class EdiParser
    {
        private readonly AppSettings _settings;

        public EdiParser(AppSettings settings)
        {
            _settings = settings;
        }

        //---------- MAIN - PARSER DE LINHAS ----------//
        public List<EdiLine> Parse(string[] fileLines, ExcelRecordGroup excelRecordGroup)
        {


            List<EdiLine> ediLines = new List<EdiLine>();
            int lineNumber = 0;

            foreach (var line in fileLines)
            {
                var lineCode = line.Substring(0, 3).Trim();
                if (!string.IsNullOrEmpty(lineCode) && lineCode.Length == 3)
                {
                    var lineConfig = _settings.EdiFieldValidation.GetListByCode(lineCode);

                    if (lineConfig != null)
                    {
                        var decodedLine = ParseLine(line, lineConfig, excelRecordGroup);

                        EdiLine ediLine = new EdiLine
                        {
                            Id = lineNumber,
                            LineCode = lineCode,
                            Columns = decodedLine,
                            
                        };

                        ediLines.Add(ediLine);
                    }
                }

                lineNumber++;
            }

            EdiFieldValidationExtensions.ValidatePeers(ediLines, excelRecordGroup);

            if(!ediLines.Any(a => a.HasErrors))
            {
                var lines322 = ediLines.Where(w => w.LineCode == "322").ToList();
                if(lines322 != null && lines322.Any())
                {
                    foreach(var line322 in lines322)
                    {
                        var knowledgeNumber = line322.Columns[3].Content;
                        var line329 = ediLines.FirstOrDefault(w => w.LineCode == "329" && w.Columns[12].Content.StartsWith(knowledgeNumber));

                        if (line329 != null)
                        {
                            var columns322 = line322.Columns;
                            var columns329 = line329.Columns;

                            EdiFieldValidationExtensions.ValidateVehicle(columns329, _settings, excelRecordGroup);
                            EdiFieldValidationExtensions.ValidateBranch(columns322, columns329, _settings, excelRecordGroup);
                            EdiFieldValidationExtensions.ValidateCollectType(columns329, _settings, excelRecordGroup);
                            EdiFieldValidationExtensions.ValidateCollectRequestCode(columns329, _settings, excelRecordGroup);
                            EdiFieldValidationExtensions.ValidateKnowledgeNumber(columns322, columns329, _settings, excelRecordGroup);

                            

                            bool line322HasErrors = columns322.Any(w => !w.IsValidated);
                            bool line329HasErrors = columns329.Any(w => !w.IsValidated);

                            if (line322HasErrors)
                            {
                                line322.HasErrors = true;
                            }

                            if (line329HasErrors)
                            {
                                line329.HasErrors = true;
                            }
                            
                        }
                    }

                    
                }

                if (excelRecordGroup != null)
                {
                    var textTotalAmount = excelRecordGroup.TotalRevenue.ToString("F2")
                        .Replace(".", string.Empty).Replace(",", string.Empty);

                    var line323 = ediLines.Where(w => w.LineCode == "323").SelectMany(s => s.Columns).ToList();
                    if (line323 != null && line323.Any())
                    {
                        EdiFieldValidationExtensions.ValidateTotalAmount(line323, textTotalAmount, excelRecordGroup);
                    }
                }
            }

            return ediLines;

            //var line322Config = _settings.EdiFieldValidation.GetListByCode("322");
            //var line329Config = _settings.EdiFieldValidation.GetListByCode("329");

            //for (int i = 0; i < fileLines.Length; i++)
            //{
            //    if (fileLines[i].StartsWith("322"))
            //    {
            //        string line322Text = fileLines[i];
            //        string line329Text = FindCorrespondingLine(fileLines, i + 1, "329");


            //        if (line322Text != null)
            //        {
            //            var column322 = ParseLine(line322Text, line322Config);
            //            var column329 = ParseLine(line329Text, line329Config);

            //            EdiFieldValidationExtensions.ValidateVehicle(column329, _settings);
            //            EdiFieldValidationExtensions.ValidateBranch(column322, column329, _settings);
            //            EdiFieldValidationExtensions.ValidateCollectType(column329, _settings);
            //            EdiFieldValidationExtensions.ValidateCollectRequestCode(column329, _settings);
            //            EdiFieldValidationExtensions.ValidateKnowledgeNumber(column322, column329, _settings);

            //            var xxx = column322.Where(w => !w.IsValidated).ToList();
            //            var yyy = column329.Where(w => !w.IsValidated).ToList();

            //            if(xxx.Count > 0 || yyy.Count > 0)
            //            {

            //            }
            //        }
            //    }
            //}
        }

        //---------- SUB-MAIN - ENCONTRAR LINHA CORRESPONDENTE ----------//
        private string FindCorrespondingLine(string[] lines, int startIndex, string prefix)
        {
            for (int i = startIndex; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(prefix)) return lines[i];
                if (lines[i].StartsWith("322") || lines[i].StartsWith("323")) return null;
            }
            return null;
        }

        //---------- DECODIFICADORES DE LINHAS ----------//


        private List<EdiColumn> ParseLine(string lineText, List<EdiFieldValidationSettings> ediFieldValidationSettings, ExcelRecordGroup excelRecordGroup)
        {
            List<EdiColumn> columns = new List<EdiColumn>();

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
                        Error = "Column position out of bounds."
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

    }
}
