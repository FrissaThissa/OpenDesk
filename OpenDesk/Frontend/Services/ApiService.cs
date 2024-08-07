using Shared.Models;
using System.Text;
using System.Text.Json;

namespace Frontend.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        HttpResponseMessage? response = await _httpClient.GetAsync($"api/{endpoint}");
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), _options);
    }

    public async Task<T?> PostAsync<T>(string endpoint, object data)
    {
        StringContent content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        HttpResponseMessage? response = await _httpClient.PostAsync($"api/{endpoint}", content);
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), _options);
    }

    public async Task<T?> PutAsync<T>(string endpoint, object data)
    {
        string json = JsonSerializer.Serialize(data);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage? response = await _httpClient.PutAsync($"api/{endpoint}", content);
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), _options);
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        HttpResponseMessage? response = await _httpClient.DeleteAsync($"api/{endpoint}");
        return response.IsSuccessStatusCode;
    }
}
