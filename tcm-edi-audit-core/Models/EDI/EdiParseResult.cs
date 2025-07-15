using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.EDI
{
    public class EdiParseResult
    {
        public List<EdiLine> Lines { get; set; } = new List<EdiLine>();
        public List<string> Errors { get; set; } = new List<string>();
        public bool Success => !Errors.Any();
    }
}
