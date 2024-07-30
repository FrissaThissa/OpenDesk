using Frontend.Models.Auth;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Newtonsoft.Json;

namespace Frontend.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService localStorage;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        this.localStorage = localStorage;
    }

    public async Task PostLogin(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/login", request);

            if (response == null)
                return;

            if (response.IsSuccessStatusCode)
            {
                request.Success = true;

                var content = await response.Content.ReadAsStringAsync();
                AuthToken? token = JsonConvert.DeserializeObject<AuthToken>(content);
                if(token != null) 
                    await localStorage.SetItemAsync("authToken", token);
            }
            else
            {
                request.ErrorMessage = "Login failed. Please check your credentials.";
            }
        }
        catch (Exception ex)
        {
            request.ErrorMessage = "Login failed. Please try again later.";
            //TODO: Log error
        }
    }
}
