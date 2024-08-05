using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages.Workspace;

public partial class Delete
{
    [Inject] public WorkspaceService WorkspaceService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public int WorkspaceId { get; set; }

    private WorkspaceDto? workspaceDto { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        workspaceDto = await WorkspaceService.GetWorkspaceById(WorkspaceId);
    }

    private async void DeleteWorkspace()
    {
        if (workspaceDto == null)
            return;

        try
        {
            await WorkspaceService.DeleteWorkspaceAsync(workspaceDto);
            NavigationManager.NavigateTo("/Workspaces");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
