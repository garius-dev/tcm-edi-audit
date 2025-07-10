using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit.Models.Settings
{
    public class SystemSettings
    {
        public string FolderPath { get; set; }
        public string ExcelPath { get; set; }
        public string SaveNewFilePath { get; set; }
    }
    public class AppSettings
    {
        public List<VehicleSettings> Vehicles { get; set; }
        public List<BranchSettings> Branches { get; set; }
        public List<CollectionSettings> Collections { get; set; }
        public List<EdiFieldValidationSettings> EdiFieldValidation { get; set; }
        //public List<Positions329Config> Positions329 { get; set; }
        //public List<FileConfigSetting> FileConfigSettings { get; set; }

        public AppSettings()
        {
            // Inicializa com valores padrão para o primeiro uso
            Vehicles = new List<VehicleSettings>();
            Branches = new List<BranchSettings>();
            Collections = new List<CollectionSettings>();
            EdiFieldValidation = new List<EdiFieldValidationSettings>();
            //Positions322 = new List<Positions322Config>();
            //Positions329 = new List<Positions329Config>();
            //FileConfigSettings = new List<FileConfigSetting>();
        }
    }
}
