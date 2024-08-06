using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages;

public partial class Index
{
    [Inject] public StateService StateService { get; set; } = default!;
    [Inject] public AuthService AuthService { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        if (StateService.User != null)
            return;

        await AuthService.TryRefreshToken();
    }
}
