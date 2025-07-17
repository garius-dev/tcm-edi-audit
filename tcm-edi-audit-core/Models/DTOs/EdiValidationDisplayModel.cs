using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.DTOs
{
    public class EdiValidationDisplayModel
    {
        [DisplayName("Arquivo")]
        public string FileName { get; set; } = string.Empty;

        [Browsable(false)]
        public string Status { get; set; } = string.Empty;

        [DisplayName("Status")]
        public Image? StatusIcon { get; set; }

        [DisplayName("Protocolo")]
        public string Protocol { get; set; } = string.Empty;

        [DisplayName("Mensagem")]
        public string Message { get; set; } = string.Empty;

    }
}
