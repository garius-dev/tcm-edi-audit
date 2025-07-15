using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.EDI.Settings
{
    public class EdiLineCodeDefinitionSettings
    {
        public string Code { get; set; }
        public int MinLen { get; set; }
        public int MaxLen { get; set; }
        public int FieldQty { get; set; }
    }
}
