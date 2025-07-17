using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcm_edi_audit_core.Models.EDI.Settings;

namespace tcm_edi_audit_core.Models.Settings
{

    public class UserSetting
    {
        [DisplayName("Nome")]
        public string UserName { get; set; }

        [DisplayName("Windows User Name")]
        public string UserAccount { get; set; }
    }
    
    public class AppSettings
    {
        public List<UserSetting> AdminUsers { get; set; } = new List<UserSetting>();

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
