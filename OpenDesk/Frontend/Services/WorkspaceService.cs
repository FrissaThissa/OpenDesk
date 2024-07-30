using Shared.Models;

namespace Frontend.Services;

public class WorkspaceService
{
    private readonly ApiService _apiService;

    public WorkspaceService(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<WorkspaceDto>?> GetWorkspacesAsync()
    {
        return await _apiService.GetAsync<IEnumerable<WorkspaceDto>>("Workspace");
    }

    public async Task<WorkspaceDto?> CreateWorkspaceAsync(WorkspaceDto workspace)
    {
        return await _apiService.PostAsync<WorkspaceDto>("Workspace", workspace);
    }

    public async Task<WorkspaceDto?> EditWorkspaceAsync(WorkspaceDto workspace)
    {
        return await _apiService.PutAsync<WorkspaceDto>("Workspace", workspace);
    }

    public async Task<bool> DeleteWorkspaceAsync(WorkspaceDto workspace)
    {
        return await _apiService.DeleteAsync($"Workspace/{workspace.Id}");
    }
}
