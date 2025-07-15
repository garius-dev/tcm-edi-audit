using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.EDI.Settings
{
    public class BranchSettings
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int OddCode { get; set; }
        public int EvenCode { get; set; }
    }
}
