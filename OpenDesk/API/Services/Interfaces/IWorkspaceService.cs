using Shared.Models;

namespace API.Services.Interfaces;

public interface IWorkspaceService
{
    Task<IEnumerable<Workspace>> GetAllWorkspacesAsync();
    Task<Workspace?> GetWorkspaceByIdAsync(int id);
}
