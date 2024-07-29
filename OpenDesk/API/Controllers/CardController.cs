using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardController : BaseController
{
    private readonly ICardService _cardService;

    public CardController(ICardService cardService, IUserService userService) : base(userService)
    {
        _cardService = cardService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        IEnumerable<CardDto> cards = await _cardService.GetAllCardsDtoAsync(user);

        return Ok(cards);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetCardById(int id)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        CardDto? card = await _cardService.GetCardDtoByIdAsync(id, user);
        if (card == null)
            return NotFound();

        return Ok(card);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CardDto card)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        await _cardService.CreateCardAsync(card, user);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Edit(CardDto card)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        await _cardService.EditCardAsync(card, user);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        ApplicationUser? user = await GetCurrentUserAsync();
        if (user == null)
            return Forbid();

        await _cardService.DeleteCardAsync(id, user);
        return Ok();
    }
}
