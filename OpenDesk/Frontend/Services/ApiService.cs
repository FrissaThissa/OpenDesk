using Newtonsoft.Json;
using Shared.Models;
using System.Text;

namespace Frontend.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        HttpResponseMessage? response = await _httpClient.GetAsync($"api/{endpoint}");
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> PostAsync<T>(string endpoint, object data)
    {
        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        HttpResponseMessage? response = await _httpClient.PostAsync($"api/{endpoint}", content);
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> PutAsync<T>(string endpoint, object data)
    {
        string json = JsonConvert.SerializeObject(data);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage? response = await _httpClient.PutAsync($"api/{endpoint}", content);
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        HttpResponseMessage? response = await _httpClient.DeleteAsync($"api/{endpoint}");
        return response.IsSuccessStatusCode;
    }
}
