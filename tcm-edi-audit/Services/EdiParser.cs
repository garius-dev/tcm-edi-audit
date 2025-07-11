using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tcm_edi_audit.Extensions;
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

            List<EdiLine> ediLines = EdiLine.DecodeLines(fileLines, _settings);

            if (ediLines.Any(a => a.HasFatalErrors))
            {
                return ediLines;
            }


            var lines322 = ediLines.GetEdiLinesByCode("322");

            if (!lines322.IsNullOrEmpty())
            {
                foreach (var line322 in lines322)
                {
                    var line329 = ediLines.Get329Peer(line322.Columns[3].Content);

                    if (line329 != null)
                    {
                        var columns322 = line322.Columns;
                        var columns329 = line329.Columns;

                        EdiFieldValidationExtensions.ValidateVehicle(columns329, _settings, excelRecordGroup);
                        EdiFieldValidationExtensions.ValidateBranch(columns322, columns329, _settings, excelRecordGroup);
                        EdiFieldValidationExtensions.ValidateCollectType(columns329, _settings, excelRecordGroup);

                        EdiFieldValidationExtensions.ValidateCollectRequestCode(columns329, _settings, excelRecordGroup);


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

            //if (excelRecordGroup != null)
            //{
            //    var totalAmount = Math.Truncate(excelRecordGroup.TotalRevenue * 100) / 100;
            //    var textTotalAmount = totalAmount.ToString("F2")
            //        .Replace(".", string.Empty).Replace(",", string.Empty);

            //    var line323 = ediLines.GetEdiLinesByCode("323").SelectMany(s => s.Columns).ToList();
            //    if (!line323.IsNullOrEmpty())
            //    {
            //        EdiFieldValidationExtensions.ValidateTotalAmount(line323, textTotalAmount, excelRecordGroup);
            //        bool line323HasErrors = line323.Any(w => !w.IsValidated);
            //        if (line323HasErrors)
            //        {
            //            ediLines.Where(w => w.LineCode == "323").ToList().ForEach(f => f.HasErrors = true);
            //        }
            //    }
            //}

            var linesWithErros = ediLines.Where(w => w.HasErrors).ToList();
            if (linesWithErros != null && linesWithErros.Any())
            {
                var errorGroup = linesWithErros
                    .SelectMany(line => line.Columns
                    .Where(col => !col.IsValidated)
                    .Select(col => new
                    {
                        LineId = line.Id,
                        ColumnId = col.Id,
                        Error = col.Error
                    }))
                    .ToList();
            }

            return ediLines;


        }




    }
}
