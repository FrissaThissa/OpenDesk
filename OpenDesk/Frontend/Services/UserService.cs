using Shared.Models;

namespace Frontend.Services;

public class UserService
{
    private readonly ApiService _apiService;

    public UserService(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        return await _apiService.GetAsync<UserDto>($"User");
    }
}
