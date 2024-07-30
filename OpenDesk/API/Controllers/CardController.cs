using API.Models.Auth;
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

    public CardController(ICardService cardService, IUserService userService)
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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CardDto card)
    {
        await _cardService.CreateCardAsync(card);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Edit(CardDto card)
    {
        await _cardService.EditCardAsync(card);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        await _cardService.DeleteCardAsync(id);
        return Ok();
    }
}
