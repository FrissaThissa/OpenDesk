using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages;

public partial class Workspaces
{
    [Inject] public WorkspaceService WorkspaceService { get; set; } = default!;

    private IEnumerable<WorkspaceDto>? workspaces = default!;

    protected override async Task OnParametersSetAsync()
    {
        this.workspaces = await GetWorkspacesAsync();
    }

    public async Task<IEnumerable<WorkspaceDto>?> GetWorkspacesAsync()
    {
        return await WorkspaceService.GetWorkspacesAsync();
    }
}
