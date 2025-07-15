using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcm_edi_audit_core.Models.EDI.Settings;

namespace tcm_edi_audit_core.Models.Settings
{
    
    public class AppSettings
    {
        public string SourceFolderPath { get; set; }
        public string ReferenceExcelFilePath { get; set; }
        public string OutputFolderPath { get; set; }

        public List<VehicleSettings> Vehicles { get; set; }
        public List<BranchSettings> Branches { get; set; }
        public List<CollectTypeSettings> CollectTypes { get; set; }
        public List<EdiFieldDefinitionSettings> EdiFieldDefinitions { get; set; }
        public List<EdiLineCodeDefinitionSettings> EdiLineCodeDefinitions { get; set; }

        public EdiLineCodeDefinitionSettings GetCodeDefinition(string code)
        {
            return EdiLineCodeDefinitions.FirstOrDefault(w => w.Code == code);
        }

        public List<EdiFieldDefinitionSettings> GetFieldDefinition(string code)
        {
            return EdiFieldDefinitions.Where(w => w.LineCode == code).ToList();
        }
    }
}
