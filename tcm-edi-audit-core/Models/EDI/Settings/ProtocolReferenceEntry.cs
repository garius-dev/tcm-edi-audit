using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.EDI.Settings
{
    public class ExcelEntry
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
    }
}
