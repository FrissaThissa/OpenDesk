﻿using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkspaceController : BaseController
{
    private readonly IWorkspaceService _workspaceService;

    public WorkspaceController(IWorkspaceService workspaceService, IUserService userService) : base(userService)
    {
        _workspaceService = workspaceService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        IEnumerable<WorkspaceDto> workspaces = await _workspaceService.GetAllWorkspacesDtoAsync(user);

        return Ok(workspaces);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetWorkspaceById(int id)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        WorkspaceDto? workspace = await _workspaceService.GetWorkspaceDtoByIdAsync(id, user);
        if (workspace == null)
            return NotFound();

        return Ok(workspace);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(WorkspaceDto workspace)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        await _workspaceService.CreateWorkspaceAsync(workspace, user);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Edit(int id, WorkspaceDto workspace)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        await _workspaceService.EditWorkspaceAsync(workspace, user);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        await _workspaceService.DeleteWorkspaceAsync(id, user);
        return Ok();
    }
}
