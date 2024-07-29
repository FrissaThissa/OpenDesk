using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardController : BaseController
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService, IUserService userService) : base(userService)
    {
        _boardService = boardService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        IEnumerable<BoardDto> boards = await _boardService.GetAllBoardsDtoAsync(user);

        return Ok(boards);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetBoardById(int id)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        BoardDto? board = await _boardService.GetBoardDtoByIdAsync(id, user);
        if (board == null)
            return NotFound();

        return Ok(board);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(BoardDto board)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        await _boardService.CreateBoardAsync(board, user);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Edit(int id, BoardDto board)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        await _boardService.EditBoardAsync(board, user);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        await _boardService.DeleteBoardAsync(id, user);
        return Ok();
    }
}
