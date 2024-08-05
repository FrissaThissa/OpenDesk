using API.Models.Auth;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardController : ControllerBase
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService) 
    {
        _boardService = boardService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        IEnumerable<BoardDto>? boards = await _boardService.GetAllBoardsDtoAsync();
        if (boards == null)
            return NotFound();

        return Ok(boards);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetBoardById(int id)
    {
        BoardDto? board = await _boardService.GetBoardDtoByIdAsync(id);
        if (board == null)
            return NotFound();

        return Ok(board);
    }

    [HttpGet("workspace/{workspaceId}")]
    [Authorize]
    public async Task<IActionResult> GetBoardsByWorkspaceId(int workspaceId)
    {
        IEnumerable<BoardDto>? boards = await _boardService.GetBoardsDtoByWorkspaceIdAsync(workspaceId);
        if (boards == null)
            return NotFound();

        return Ok(boards);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(BoardDto board)
    {
        BoardDto? dto = await _boardService.CreateBoardAsync(board);
        if(dto == null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Edit(BoardDto board)
    {
        BoardDto? dto = await _boardService.EditBoardAsync(board);
        if (dto == null)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        await _boardService.DeleteBoardAsync(id);
        return Ok();
    }
}
