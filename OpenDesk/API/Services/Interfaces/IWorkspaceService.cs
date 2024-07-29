using API.Models;
using Shared.Models;

namespace API.Services.Interfaces;

public interface IWorkspaceService
{
    Task<IEnumerable<WorkspaceDto>> GetAllWorkspacesDtoAsync(ApplicationUser user);
    Task<WorkspaceDto?> GetWorkspaceDtoByIdAsync(int id, ApplicationUser user);
    Task CreateWorkspaceAsync(WorkspaceDto workspace, ApplicationUser user);
    Task EditWorkspaceAsync(WorkspaceDto workspace, ApplicationUser user);
    Task DeleteWorkspaceAsync(int id, ApplicationUser user);
}
