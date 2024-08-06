using Frontend.Services;
using Microsoft.AspNetCore.Components;

namespace Frontend;

public partial class App
{
    [Inject] public StateService StateService { get; set; } = default!;
    [Inject] public AuthService AuthService { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        if (StateService.User != null)
            return;

        await AuthService.StartRefreshAsync(initialRefresh: true);
    }
}
