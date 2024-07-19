using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace API.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly OpenDeskContext _context;

    public WorkspaceService(OpenDeskContext context)
    {
        _context = context; 
    }

    public async Task<IEnumerable<Workspace>> GetAllWorkspacesAsync()
    {
        return await _context.Workspaces.Include(w => w.Boards)
            .ThenInclude(b => b.Cards)
            .ToListAsync();
    }

    public async Task<Workspace?> GetWorkspaceByIdAsync(int id)
    {
        return await _context.Workspaces.Include(w => w.Boards)
            .ThenInclude(b => b.Cards)
            .FirstOrDefaultAsync(w => w.Id == id);
    }
}
