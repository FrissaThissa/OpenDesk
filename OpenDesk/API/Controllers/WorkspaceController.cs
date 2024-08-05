using API.Models.Auth;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

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
        IEnumerable<WorkspaceDto>? workspaces = await _workspaceService.GetAllWorkspacesDtoAsync();
        return Ok(workspaces);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetWorkspaceById(int id)
    {
        WorkspaceDto? workspace = await _workspaceService.GetWorkspaceDtoByIdAsync(id);
        if (workspace == null)
            return NotFound();

        return Ok(workspace);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(WorkspaceDto workspace)
    {
        WorkspaceDto? dto = await _workspaceService.CreateWorkspaceAsync(workspace);
        if (dto == null)
            return NotFound();
        return Ok(dto);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Edit(WorkspaceDto workspace)
    {
        WorkspaceDto? dto = await _workspaceService.EditWorkspaceAsync(workspace);
        if (dto == null)
            return NotFound();
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        await _workspaceService.DeleteWorkspaceAsync(id);
        return Ok();
    }
}
