using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.EDI
{
    public class EdiColumn
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool Success { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
