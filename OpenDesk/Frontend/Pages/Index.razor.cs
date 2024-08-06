using Frontend.Services;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public partial class Index
{
    [Inject] public StateService StateService { get; set; } = default!;

    protected override void OnInitialized()
    {
        StateService.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        StateService.OnChange -= StateHasChanged;
    }
}
