using API.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkspaceController : ControllerBase
{
    private readonly WorkspaceService _workspaceService;

    public WorkspaceController(WorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    [HttpGet]
    public async Task<IEnumerable<Workspace>> Get()
    {
        return await _workspaceService.GetAllWorkspacesAsync();
    }

    [HttpGet]
    public async Task<Workspace?> GetWorkspaceById(int id)
    {
        return await _workspaceService.GetWorkspaceByIdAsync(id);
    }
}
