using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages;

public partial class Workspaces
{
    [Inject] public WorkspaceService WorkspaceService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private IEnumerable<WorkspaceDto>? workspaces = default!;

    protected override async Task OnParametersSetAsync()
    {
        this.workspaces = await WorkspaceService.GetWorkspacesAsync();
    }

    private void NavigateToBoards(int workspaceId)
    {
        NavigationManager.NavigateTo($"/boards/{workspaceId}");
    }
}
