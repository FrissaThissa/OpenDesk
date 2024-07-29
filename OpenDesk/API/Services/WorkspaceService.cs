using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Shared.Models;

namespace API.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly OpenDeskContext _context;

    public WorkspaceService(OpenDeskContext context)
    {
        _context = context; 
    }

    public async Task<IEnumerable<WorkspaceDto>> GetAllWorkspacesDtoAsync(ApplicationUser user)
    {
        List<Workspace> workspaces = await _context.Workspaces.Include(w => w.Boards)
            .ThenInclude(b => b.Cards)
            .Where(w => w.OwnerId == user.Id)
            .ToListAsync();

        return ConvertListToDto(workspaces);
    }

    public async Task<WorkspaceDto?> GetWorkspaceDtoByIdAsync(int id, ApplicationUser user)
    {
        Workspace? workspace = await _context.Workspaces.Include(w => w.Boards)
            .ThenInclude(b => b.Cards)
            .Where(w => w.OwnerId == user.Id)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workspace == null)
            return null;

        return ConvertObjectToDto(workspace);
    }

    public async Task CreateWorkspaceAsync(WorkspaceDto dto, ApplicationUser user)
    {
        Workspace workspace = ConvertDtoToObject(dto);

        workspace.Owner = user;

        _context.Workspaces.Add(workspace);
        await _context.SaveChangesAsync();
    }

    public async Task EditWorkspaceAsync(WorkspaceDto dto, ApplicationUser user)
    {
        Workspace workspace = ConvertDtoToObject(dto);

        Workspace? dbWorkspace = await GetWorkspaceByIdAsync(workspace.Id, user);
        if (dbWorkspace == null)
            return;

        _context.Workspaces.Update(workspace);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWorkspaceAsync(int id, ApplicationUser user)
    {
        Workspace? workspace = await GetWorkspaceByIdAsync(id, user);
        if (workspace == null)
            return;

        _context.Workspaces.Remove(workspace);
        await _context.SaveChangesAsync();
    }

    private async Task<Workspace?> GetWorkspaceByIdAsync(int id, ApplicationUser user)
    {
        return await _context.Workspaces.Include(w => w.Boards)
            .ThenInclude(b => b.Cards)
            .Where(w => w.OwnerId == user.Id)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    private WorkspaceDto ConvertObjectToDto(Workspace workspace)
    {
        WorkspaceDto dto = new WorkspaceDto()
        {
            Id = workspace.Id,
            Name = workspace.Name,
            OwnerId = workspace.Owner.Id,
            OwnerEmail = workspace.Owner.Email
        };
        return dto;
    }

    private List<WorkspaceDto> ConvertListToDto(List<Workspace> workspaces)
    {
        return workspaces.Select(ConvertObjectToDto).ToList();
    }

    private Workspace ConvertDtoToObject(WorkspaceDto dto)
    {
        Workspace workspace = new Workspace()
        {
            Id = dto.Id,
            Name = dto.Name,
            OwnerId = dto.OwnerId
        };
        return workspace;
    }
}
