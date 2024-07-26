using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Diagnostics;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkspaceController : ControllerBase
{
    private readonly IWorkspaceService _workspaceService;

    public WorkspaceController(IWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        IEnumerable<Workspace> workspaces = await _workspaceService.GetAllWorkspacesAsync();

        return Ok(workspaces);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkspaceById(int id)
    {
        Workspace? workspace = await _workspaceService.GetWorkspaceByIdAsync(id);
        if (workspace == null)
            return NotFound();

        return Ok(workspace);
    }
}
