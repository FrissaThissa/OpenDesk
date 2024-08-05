using API.Models.Auth;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;

    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        IEnumerable<CardDto>? cards = await _cardService.GetAllCardsDtoAsync();
        return Ok(cards);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetCardById(int id)
    {
        CardDto? card = await _cardService.GetCardDtoByIdAsync(id);
        if (card == null)
            return NotFound();

        return Ok(card);
    }

    [HttpGet("board/{boardId}")]
    [Authorize]
    public async Task<IActionResult> GetCardsByBoardId(int boardId)
    {
        var boards = await _cardService.GetCardsDtoByBoardIdAsync(boardId);
        return Ok(boards);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CardDto card)
    {
        CardDto? dto = await _cardService.CreateCardAsync(card);
        if (dto == null)
            return Forbid();
        return Ok(dto);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Edit(CardDto card)
    {
        CardDto? dto = await _cardService.EditCardAsync(card);
        if (dto == null)
            return Forbid();
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        await _cardService.DeleteCardAsync(id);
        return Ok();
    }
}
