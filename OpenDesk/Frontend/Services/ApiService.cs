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
        if (!response.IsSuccessStatusCode)
            return default;
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> PostAsync<T>(string endpoint, object data)
    {
        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        HttpResponseMessage? response = await _httpClient.PostAsync($"api/{endpoint}", content);
        if (!response.IsSuccessStatusCode)
            return default;
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> PutAsync<T>(string endpoint, object data)
    {
        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        HttpResponseMessage? response = await _httpClient.PutAsync($"api/{endpoint}", content);
        if (!response.IsSuccessStatusCode)
            return default;
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        HttpResponseMessage? response = await _httpClient.DeleteAsync(endpoint);
        return response.IsSuccessStatusCode;
    }
}
