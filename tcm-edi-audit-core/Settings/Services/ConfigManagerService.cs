using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using tcm_edi_audit_core.Models.Settings;
using tcm_edi_audit_core.Properties;

namespace tcm_edi_audit_core.Settings.Services
{
    public class ConfigManagerService
    {
        private readonly string ConfigFilePath = Path.Combine(Application.StartupPath, "settings.json");
        private readonly string ConfigLocalFilePath = Path.Combine(Application.StartupPath, "local-settings.json");

        private readonly FirebaseService _firebaseService;

        public ConfigManagerService()
        {
            _firebaseService = new FirebaseService();
        }

        public async Task<AppSettings> LoadSettingsFromCloud()
        {
            string configResponse = await _firebaseService.GetCachedOrUpdatedConfigAsync("AIzaSyAoBr5P2NQifKQVbKHNSsxHMZr0uvdc7vs");
            return JsonConvert.DeserializeObject<AppSettings>(configResponse);
        }

        public async Task<bool> SaveSettingsToCloud(object newConfig)
        {
            string idToken = await _firebaseService.GetAnonymousIdTokenAsync("AIzaSyAoBr5P2NQifKQVbKHNSsxHMZr0uvdc7vs");
            return await _firebaseService.UpdateConfigJsonAsync(idToken, newConfig);
        }

        public AppSettingsLocal LoadSettings()
        {
            if (File.Exists(ConfigLocalFilePath))
            {
                try
                {
                    string json = File.ReadAllText(ConfigLocalFilePath);
                    return JsonConvert.DeserializeObject<AppSettingsLocal>(json);
                }
                catch
                {
                    return CreateDefaultSettings();
                }
            }
            else
            {
                return CreateDefaultSettings();
            }
        }

        public void SaveSettings(AppSettingsLocal settings)
        {
            try
            {
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(ConfigLocalFilePath, json);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao salvar as configurações: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private AppSettingsLocal CreateDefaultSettings()
        {
            var defaultSettings = new AppSettingsLocal();
            
            SaveSettings(defaultSettings);
            return defaultSettings;
        }
    }
}
