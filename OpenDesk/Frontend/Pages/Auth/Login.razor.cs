using Frontend.Models.Auth;
using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;
using System.Net.Http.Json;

namespace Frontend.Pages.Auth;

public partial class Login
{
    private LoginRequest loginRequest { get; set; } = new LoginRequest();

    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public AuthService AuthService { get; set; } = default!;
    [Inject] public UserService UserService { get; set; } = default!;
    [Inject] public StateService StateService { get; set; } = default!;

    private async Task PostLogin()
    {
        await AuthService.PostLogin(loginRequest);
        if (!loginRequest.Success)
            return;

        Navigation.NavigateTo("/");
    }
}