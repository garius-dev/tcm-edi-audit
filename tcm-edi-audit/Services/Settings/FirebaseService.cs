using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class FirebaseService
{
    private static readonly string FirebaseBaseUrl = "https://tcmsharedsettings-default-rtdb.firebaseio.com/.json";
    private static readonly string LocalCachePath = "config-cache.json";
    private static string _cachedJson;
    private static string _lastHash;
    private static DateTime _lastChecked = DateTime.MinValue;
    private static readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Realiza autenticação anônima e retorna o token do Firebase.
    /// </summary>
    public static async Task<string> GetAnonymousIdTokenAsync(string apiKey)
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

    /// <summary>
    /// Recupera o JSON completo de configuração do Firebase.
    /// </summary>
    public static async Task<string> GetConfigJsonAsync(string idToken)
    {
        var client = new HttpClient();
        var url = $"{FirebaseBaseUrl}?auth={idToken}";

        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Atualiza a configuração no Firebase e salva localmente.
    /// </summary>
    public static async Task<bool> UpdateConfigJsonAsync(string idToken, object newConfig)
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

    /// <summary>
    /// Carrega o JSON do cache local, se existir.
    /// </summary>
    private static void LoadFromDiskCache()
    {
        if (File.Exists(LocalCachePath))
        {
            _cachedJson = File.ReadAllText(LocalCachePath);
            _lastHash = ComputeSha256Hash(_cachedJson);
            _lastChecked = DateTime.Now;
        }
    }

    /// <summary>
    /// Retorna o JSON em cache ou atualiza do Firebase se necessário.
    /// </summary>
    public static async Task<string> GetCachedOrUpdatedConfigAsync(string apiKey)
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

    /// <summary>
    /// Gera o hash SHA256 de um texto.
    /// </summary>
    private static string ComputeSha256Hash(string rawData)
    {
        var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return BitConverter.ToString(bytes).Replace("-", "");
    }
}
