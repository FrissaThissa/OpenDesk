using Frontend.Models.Auth;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Shared.Models;
using System.Text.Json;

namespace Frontend.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly StateService _stateService;
    private readonly UserService _userService;

    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly TimeSpan _refreshBuffer = TimeSpan.FromMinutes(5); // Refresh 5 minutes before expiry
    private Timer? _timer = null;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage, StateService stateService, UserService userService)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _stateService = stateService;
        _userService = userService;
    }

    public async Task Login(LoginRequest request)
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
                AuthToken? token = JsonSerializer.Deserialize<AuthToken>(content, _options);
                if (token == null)
                    return;

                await _localStorage.SetItemAsync("authToken", token);

                UserDto? user = await _userService.GetCurrentUserAsync();
                if (user == null)
                    return;

                _stateService.User = user;

                await StartRefreshAsync();
            }
            else
            {
                request.ErrorMessage = "Login failed. Please check your credentials.";
            }
        }
        catch (Exception ex)
        {
            request.ErrorMessage = "Login failed. Please try again later.";
            Console.WriteLine(ex);
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        if (_timer != null)
        {
            _timer.Dispose();
            _timer = null;
        }
        _stateService.User = null;
    }

    public async Task StartRefreshAsync(bool initialRefresh = false)
    {
        if (_timer != null)
            return;
        if(initialRefresh)
            TryRefreshToken(null);
        await SetTimerAsync();
    }

    private async Task SetTimerAsync()
    {
        var token = await _localStorage.GetItemAsync<AuthToken>("authToken");
        if (token == null)
            return;

        var expiresIn = TimeSpan.FromSeconds(token.ExpiresIn);
        var refreshTime = expiresIn - _refreshBuffer;
        _timer = new Timer(TryRefreshToken, null, refreshTime, Timeout.InfiniteTimeSpan);
    }

    private async void TryRefreshToken(object? state)
    {
        AuthToken? token = await _localStorage.GetItemAsync<AuthToken>("authToken");
        if (token == null)
            return;

        RefreshRequest request = new RefreshRequest()
        {
            RefreshToken = token.RefreshToken
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("/refresh", request);

            if (response == null)
                return;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                AuthToken? newtoken = JsonSerializer.Deserialize<AuthToken>(content, _options);
                if (newtoken == null)
                    return;

                await _localStorage.SetItemAsync("authToken", newtoken);

                UserDto? user = await _userService.GetCurrentUserAsync();
                if (user == null)
                    return;

                _stateService.User = user;

                await SetTimerAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
