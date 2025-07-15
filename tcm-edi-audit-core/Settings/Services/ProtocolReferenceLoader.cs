using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcm_edi_audit_core.Models.EDI.Settings;

namespace tcm_edi_audit_core.Settings.Services
{
    public class ProtocolReferenceLoader
    {
        public List<ProtocolReferenceEntry> LoadFromExcel(string filePath)
        {
            var records = new List<ProtocolReferenceEntry>();

            string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{Path.GetExtension(filePath)}");
            File.Copy(filePath, tempFilePath, overwrite: true);

            try
            {
                using (var workbook = new XLWorkbook(tempFilePath))
                {
                    var worksheet = workbook.Worksheets.First();
                    var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header

                    foreach (var row in rows)
                    {
                        records.Add(new ProtocolReferenceEntry
                        {
                            Flow = row.Cell(1).GetString(),
                            State = row.Cell(2).GetString(),
                            DistanceKm = TryParseInt(row.Cell(3).GetString()),
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
                        });
                    }
                }
            }
            finally
            {
                try { File.Delete(tempFilePath); } catch { /* Ignore cleanup errors */ }
            }

            return records;
        }

        private int TryParseInt(string input)
        {
            return int.TryParse(input, out var result) ? result : 0;
        }

        private decimal ParseCurrency(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            input = input.Replace("R$", "").Trim();

            return decimal.TryParse(input, NumberStyles.Any, new CultureInfo("pt-BR"), out var result)
                ? result
                : 0;
        }
    }
}
