using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.EDI
{
    public enum EdiValidationStatus
    {
        Success = 0,
        Warning = 1,
        Error = 2,
    }

    public class EdiValidationResult
    {
        public EdiValidationStatus Status { get; set; }
        public Image? StatusIcon { get; set; }
        public FileInfo? File { get; set; }
        public string Protocol { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Warnings { get; set; } = new List<string>();
        public List<EdiLine> EdiLines { get; set; } = new List<EdiLine>();
    }
}
