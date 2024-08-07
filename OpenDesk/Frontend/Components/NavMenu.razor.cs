using Frontend.Services;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public partial class NavMenu
{
    [Inject] public StateService StateService { get; set; } = default!;
    [Inject] public AuthService AuthService { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;

    public async Task Logout()
    {
        await AuthService.Logout();
        Navigation.NavigateTo("/");
    }

    protected override void OnInitialized()
    {
        StateService.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        StateService.OnChange -= StateHasChanged;
    }
}
