using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit.Models.Settings
{
    public class ExcelRecordGroup
    {
        public string InvoiceNumber { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<TransportRecord> Records { get; set; } = new List<TransportRecord>();
    }
    public class TransportRecord
    {
        public string Flow { get; set; }
        public string State { get; set; }
        public int DistanceKm { get; set; }
        public string ScheduledVehicle { get; set; }
        public string RequestCode { get; set; }
        public string Cva { get; set; }
        public string CtePackage { get; set; }
        public string MinutePackage { get; set; }
        public string CtePiece { get; set; }
        public string MinutePiece { get; set; }
        public decimal TotalRevenue { get; set; }
        public string Invoice { get; set; }
        public string Protocol { get; set; }

        public static List<TransportRecord> ReadExcelFile(string filePath)
        {
            var records = new List<TransportRecord>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheets.First();
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // skip header

                foreach (var row in rows)
                {
                    var record = new TransportRecord
                    {
                        Flow = row.Cell(1).GetString(),
                        State = row.Cell(2).GetString(),
                        DistanceKm = int.TryParse(row.Cell(3).GetString(), out var km) ? km : 0,
                        ScheduledVehicle = row.Cell(4).GetString(),
                        RequestCode = row.Cell(5).GetString(),
                        Cva = row.Cell(6).GetString(),
                        CtePackage = row.Cell(7).GetString(),
                        MinutePackage = row.Cell(8).GetString(),
                        CtePiece = row.Cell(9).GetString(),
                        MinutePiece = row.Cell(10).GetString(),
                        TotalRevenue = ParseCurrency(row.Cell(11).GetString()),
                        Invoice = row.Cell(12).GetString(),
                        Protocol = row.Cell(13).GetString()
                    };

                    records.Add(record);
                }
            }

            return records;
        }

        public static decimal ParseCurrency(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            input = input.Replace("R$", "").Trim();

            if (decimal.TryParse(input, NumberStyles.Any, new CultureInfo("pt-BR"), out var result))
                return result;

            return 0;
        }
    }

}
