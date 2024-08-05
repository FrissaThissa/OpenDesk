using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages.Workspace;

public partial class Add
{
    [Inject] public WorkspaceService WorkspaceService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private WorkspaceDto workspaceDto { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        workspaceDto = new WorkspaceDto();
    }

    private async void PostWorkspace()
    {
        try
        {
            await WorkspaceService.CreateWorkspaceAsync(workspaceDto);
            NavigationManager.NavigateTo("/Workspaces");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}