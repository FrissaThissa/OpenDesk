using Shared.Models;

namespace API.Services.Interfaces;

public interface IWorkspaceService
{
    Task<IEnumerable<WorkspaceDto>?> GetAllWorkspacesDtoAsync();
    Task<WorkspaceDto?> GetWorkspaceDtoByIdAsync(int id);
    Task CreateWorkspaceAsync(WorkspaceDto workspace);
    Task EditWorkspaceAsync(WorkspaceDto workspace);
    Task DeleteWorkspaceAsync(int id);
}
