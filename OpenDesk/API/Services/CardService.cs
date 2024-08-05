using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Shared.Models;
using API.Models.Auth;

namespace API.Services;

public class CardService : ICardService
{
    private readonly OpenDeskContext _context;
    private readonly IUserService _userService;

    public CardService(OpenDeskContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<IEnumerable<CardDto>?> GetAllCardsDtoAsync()
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        List<Card> cards = await _context.Cards.Include(c => c.CreatedBy)
            .Where(c => c.Board.Members.Any(m => m.UserId == user.Id))
            .ToListAsync();

        return ConvertListToDto(cards);
    }

    public async Task<CardDto?> GetCardDtoByIdAsync(int id)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        Card? card = await _context.Cards.Include(c => c.CreatedBy)
            .Where(c => c.Board.Members.Any(m => m.UserId == user.Id))
            .FirstOrDefaultAsync(x => x.Id == id);

        if (card == null)
            return null;

        return ConvertObjectToDto(card);
    }

    public async Task<IEnumerable<CardDto>?> GetCardsDtoByBoardIdAsync(int id)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        List<Card> cards =  await _context.Cards.Include(c => c.CreatedBy)
            .Where(c => c.BoardId == id)
            .Where(c => c.Board.Members.Any(m => m.UserId == user.Id))
            .ToListAsync();

        return ConvertListToDto(cards);
    }

    public async Task<IEnumerable<CardDto>?> GetCardsDtoByWorkspaceIdAsync(int id)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        List<Card> cards =  await _context.Cards.Include(c => c.CreatedBy)
            .Where(c => c.Board.WorkspaceId == id)
            .Where(c => c.Board.Members.Any(m => m.UserId == user.Id))
            .ToListAsync();

        return ConvertListToDto(cards);
    }

    public async Task<CardDto?> CreateCardAsync(CardDto dto)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        Card card = ConvertDtoToObject(dto);

        card.CreatedBy = user;

        _context.Cards.Add(card);
        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task<CardDto?> EditCardAsync(CardDto dto)
    {
        Card? card = await GetCardByIdAsync(dto.Id);
        if (card == null)
            return null;

        card.Title = dto.Title;

        _context.Cards.Update(card);
        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task DeleteCardAsync(int id)
    {
        Card? card = await GetCardByIdAsync(id);
        if (card == null)
            return;

        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();
    }

    private async Task<Card?> GetCardByIdAsync(int id)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        return await _context.Cards.Include(c => c.CreatedBy)
            .Where(c => c.Board.Members.Any(m => m.UserId == user.Id))
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    private CardDto ConvertObjectToDto(Card card)
    {
        CardDto dto = new CardDto()
        {
            Id = card.Id,
            Title = card.Title,
            Description = card.Description,
            CreatedDate = card.CreatedDate,
            CreatedBy = new UserDto()
            {
                Id = card.CreatedBy.Id,
                Name = card.CreatedBy.UserName,
                Email = card.CreatedBy.Email
            },
            BoardId = card.BoardId
        };
        return dto;
    }

    private List<CardDto> ConvertListToDto(List<Card> cards)
    {
        return cards.Select(ConvertObjectToDto).ToList();
    }

    private Card ConvertDtoToObject(CardDto dto)
    {
        Card card = new Card()
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            CreatedDate = dto.CreatedDate,
            BoardId = dto.BoardId
        };
        return card;
    }
}
