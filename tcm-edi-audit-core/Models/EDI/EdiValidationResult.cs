using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.EDI
{
    public class EdiValidationResult
    {
        public string FileName { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Warnings { get; set; } = new List<string>();
        public bool Success => !Errors.Any();
        public bool Warning => !Warnings.Any();
    }
}
