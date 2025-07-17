using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Models.Settings
{
    public class AppSettingsLocal
    {
        public string SourceFolderPath { get; set; }
        public string ReferenceExcelFilePath { get; set; }
        public string OutputFolderPath { get; set; }
        public bool TryFixIt { get; set; }
    }
}
