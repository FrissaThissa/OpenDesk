using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages.Workspace;

public partial class Index
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

    private void CreateWorkspace()
    {
        NavigationManager.NavigateTo("/workspace/add");
    }

    private void EditWorkspace(int workspaceId)
    {
        NavigationManager.NavigateTo($"/workspace/edit/{workspaceId}");
    }

    private void DeleteWorkspace(int workspaceId)
    {
        NavigationManager.NavigateTo($"/workspace/delete/{workspaceId}");
    }
}
