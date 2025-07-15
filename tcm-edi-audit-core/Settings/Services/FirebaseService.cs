using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace tcm_edi_audit_core.Settings.Services
{
    public class FirebaseService
    {
        private readonly string FirebaseBaseUrl = "https://tcmsharedsettings-default-rtdb.firebaseio.com/.json";
        private readonly string LocalCachePath = "config-cache.json";
        private string _cachedJson;
        private string _lastHash;
        private DateTime _lastChecked = DateTime.MinValue;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);

        public async Task<string> GetAnonymousIdTokenAsync(string apiKey)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(
                $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return obj?["idToken"]?.ToString();
        }

        public async Task<string> GetConfigJsonAsync(string idToken)
        {
            var client = new HttpClient();
            var url = $"{FirebaseBaseUrl}?auth={idToken}";

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> UpdateConfigJsonAsync(string idToken, object newConfig)
        {
            var client = new HttpClient();
            var url = $"{FirebaseBaseUrl}?auth={idToken}";

            var jsonContent = JsonConvert.SerializeObject(newConfig, Formatting.Indented);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, content);
            if (!response.IsSuccessStatusCode)
                return false;

            _cachedJson = jsonContent;
            _lastHash = ComputeSha256Hash(jsonContent);
            _lastChecked = DateTime.Now;
            File.WriteAllText(LocalCachePath, _cachedJson);

            return true;
        }

        private void LoadFromDiskCache()
        {
            if (File.Exists(LocalCachePath))
            {
                _cachedJson = File.ReadAllText(LocalCachePath);
                _lastHash = ComputeSha256Hash(_cachedJson);
                _lastChecked = DateTime.Now;
            }
        }

        public async Task<string> GetCachedOrUpdatedConfigAsync(string apiKey)
        {
            if (_cachedJson == null)
                LoadFromDiskCache();

            if (_cachedJson != null && DateTime.Now - _lastChecked < _checkInterval)
                return _cachedJson;

            var idToken = await GetAnonymousIdTokenAsync(apiKey);
            if (idToken == null)
                return _cachedJson;

            var latestJson = await GetConfigJsonAsync(idToken);
            if (latestJson == null)
                return _cachedJson;

            var latestHash = ComputeSha256Hash(latestJson);

            if (_lastHash != latestHash)
            {
                _cachedJson = latestJson;
                _lastHash = latestHash;
                _lastChecked = DateTime.Now;
                File.WriteAllText(LocalCachePath, _cachedJson);
            }

            return _cachedJson;
        }

        private string ComputeSha256Hash(string rawData)
        {
            var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(bytes).Replace("-", "");
        }


    }
}
