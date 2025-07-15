using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.EDI
{
    public class EdiLine
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        public List<EdiColumn> Columns { get; set; } = new List<EdiColumn>();
    }
}
