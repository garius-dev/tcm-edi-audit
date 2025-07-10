using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using tcm_edi_audit.Models.Settings;

namespace tcm_edi_audit.Services.Settings
{
    public static class SystemConfigManager
    {
        private static readonly string ConfigFilePath = Path.Combine(Application.StartupPath, "system-settings.json");

        public static SystemSettings LoadSettings()
        {
            if (File.Exists(ConfigFilePath))
            {
                try
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    return JsonConvert.DeserializeObject<SystemSettings>(json);
                }
                catch // Em caso de arquivo corrompido ou erro de leitura
                {
                    // Retorna configurações padrão se houver erro
                    return CreateDefaultSettings();
                }
            }
            else
            {
                return CreateDefaultSettings();
            }
        }

        public static void SaveSettings(SystemSettings settings)
        {
            try
            {
                // O Formatting.Indented torna o arquivo JSON legível para humanos
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(ConfigFilePath, json);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao salvar as configurações: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static SystemSettings CreateDefaultSettings()
        {
            var defaultSettings = new SystemSettings();
            // Podemos popular com um exemplo inicial
            //defaultSettings.Vehicles.Add(new VehicleConfig { Name = "Carreta 3 eixos", Code = "C3" });
            //defaultSettings.Branches.Add(new BranchConfig { Name = "São Bernado do Campo", OddCode = 1, EvenCode = 2, EdiCode = "SÃO BERNAR" });
            //defaultSettings.Collections.Add(new CollectionConfig { Name = "Centro de Consolidação de Carga", Code = "CCC" });



            SaveSettings(defaultSettings); // Salva o novo arquivo padrão
            return defaultSettings;
        }
    }

    public static class ConfigManager
    {
        private static readonly string ConfigFilePath = Path.Combine(Application.StartupPath, "settings.json");

        public static AppSettings LoadSettings()
        {
            if (File.Exists(ConfigFilePath))
            {
                try
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    return JsonConvert.DeserializeObject<AppSettings>(json);
                }
                catch // Em caso de arquivo corrompido ou erro de leitura
                {
                    // Retorna configurações padrão se houver erro
                    return CreateDefaultSettings();
                }
            }
            else
            {
                return CreateDefaultSettings();
            }
        }

        public static void SaveSettings(AppSettings settings)
        {
            try
            {
                // O Formatting.Indented torna o arquivo JSON legível para humanos
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(ConfigFilePath, json);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao salvar as configurações: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static AppSettings CreateDefaultSettings()
        {
            var defaultSettings = new AppSettings();
            // Podemos popular com um exemplo inicial
            //defaultSettings.Vehicles.Add(new VehicleConfig { Name = "Carreta 3 eixos", Code = "C3" });
            //defaultSettings.Branches.Add(new BranchConfig { Name = "São Bernado do Campo", OddCode = 1, EvenCode = 2, EdiCode = "SÃO BERNAR" });
            //defaultSettings.Collections.Add(new CollectionConfig { Name = "Centro de Consolidação de Carga", Code = "CCC" });



            SaveSettings(defaultSettings); // Salva o novo arquivo padrão
            return defaultSettings;
        }
    }
}