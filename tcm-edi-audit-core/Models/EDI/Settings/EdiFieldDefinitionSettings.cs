using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.EDI.Settings
{
    public class EdiFieldDefinitionSettings
    {
        public int ColumnId { get; set; }
        public string LineCode { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public int TextStartPosition { get; set; }
        public int TextLength { get; set; }
        public int Dec { get; set; }


        public int ExpectedCharCount { get; set; } = 0;
        public string AllowedText { get; set; } = string.Empty;
        public string AllowedFormat { get; set; } = string.Empty;
        public string AllowedRegex { get; set; } = string.Empty;
    }
}
