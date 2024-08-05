using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Shared.Models;
using API.Models.Auth;

namespace API.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly OpenDeskContext _context;
    private readonly IUserService _userService;

    public WorkspaceService(OpenDeskContext context, IUserService userService) 
    {
        _context = context; 
        _userService = userService;
    }

    public async Task<IEnumerable<WorkspaceDto>?> GetAllWorkspacesDtoAsync()
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        List<Workspace> workspaces = await _context.Workspaces.Include(w => w.CreatedBy)
            .Include(w => w.Boards)
            .ThenInclude(b => b.Cards)
            .Where(w => w.Members.Any(m => m.UserId == user.Id))
            .ToListAsync();

        return ConvertListToDto(workspaces);
    }

    public async Task<WorkspaceDto?> GetWorkspaceDtoByIdAsync(int id)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        Workspace? workspace = await _context.Workspaces.Include(w => w.CreatedBy)
            .Include(w => w.Boards)
            .ThenInclude(b => b.Cards)
            .Where(w => w.Members.Any(m => m.UserId == user.Id))
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workspace == null)
            return null;

        return ConvertObjectToDto(workspace);
    }

    public async Task<WorkspaceDto?> CreateWorkspaceAsync(WorkspaceDto dto)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        Workspace workspace = ConvertDtoToObject(dto);
        workspace.CreatedBy = user;
        _context.Workspaces.Add(workspace);

        WorkspaceMember member = new WorkspaceMember()
        {
            Workspace = workspace,
            User = user
        };
        _context.WorkspaceMembers.Add(member);

        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task<WorkspaceDto?> EditWorkspaceAsync(WorkspaceDto dto)
    {
        Workspace? workspace = await GetWorkspaceByIdAsync(dto.Id);
        if (workspace == null)
            return dto;

        workspace.Name = dto.Name;

        _context.Workspaces.Update(workspace);
        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task DeleteWorkspaceAsync(int id)
    {
        Workspace? workspace = await GetWorkspaceByIdAsync(id);
        if (workspace == null)
            return;

        _context.Workspaces.Remove(workspace);
        await _context.SaveChangesAsync();
    }

    private async Task<Workspace?> GetWorkspaceByIdAsync(int id)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        return await _context.Workspaces.Include(w => w.CreatedBy)
            .Include(w => w.Boards)
            .ThenInclude(b => b.Cards)
            .Where(w => w.Members.Any(m => m.UserId == user.Id))
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    private WorkspaceDto ConvertObjectToDto(Workspace workspace)
    {
        WorkspaceDto dto = new WorkspaceDto()
        {
            Id = workspace.Id,
            Name = workspace.Name,
            CreatedBy = new UserDto()
            {
                Id = workspace.CreatedBy.Id,
                Name = workspace.CreatedBy.UserName,
                Email = workspace.CreatedBy.Email
            }
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
            Name = dto.Name
        };
        return workspace;
    }
}
