using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Shared.Models;
using API.Models.Auth;

namespace API.Services;

public class CardService : ServiceBase, ICardService
{
    private readonly OpenDeskContext _context;

    public CardService(OpenDeskContext context, IHttpContextAccessor httpContextAccessor, IUserService userService) 
        : base(httpContextAccessor, userService)
    {
        _context = context;
    }

    public async Task<IEnumerable<CardDto>?> GetAllCardsDtoAsync()
    {
        ApplicationUser? user = await GetCurrentUser();
        if (user == null)
            return null;

        List<Card> cards = await _context.Cards.Include(c => c.CreatedBy)
            .Where(c => c.Board.Members.Any(m => m.UserId == user.Id))
            .ToListAsync();

        return ConvertListToDto(cards);
    }

    public async Task<CardDto?> GetCardDtoByIdAsync(int id)
    {
        ApplicationUser? user = await GetCurrentUser();
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
        ApplicationUser? user = await GetCurrentUser();
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
        ApplicationUser? user = await GetCurrentUser();
        if (user == null)
            return null;

        List<Card> cards =  await _context.Cards.Include(c => c.CreatedBy)
            .Where(c => c.Board.WorkspaceId == id)
            .Where(c => c.Board.Members.Any(m => m.UserId == user.Id))
            .ToListAsync();

        return ConvertListToDto(cards);
    }

    public async Task CreateCardAsync(CardDto dto)
    {
        ApplicationUser? user = await GetCurrentUser();
        if (user == null)
            return;

        Card card = ConvertDtoToObject(dto);

        card.CreatedBy = user;

        _context.Cards.Add(card);
        await _context.SaveChangesAsync();
    }

    public async Task EditCardAsync(CardDto dto)
    {
        Card card = ConvertDtoToObject(dto);

        Card? dbCard = await GetCardByIdAsync(card.Id);
        if (dbCard == null)
            return;

        _context.Cards.Update(card);
        await _context.SaveChangesAsync();
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
        ApplicationUser? user = await GetCurrentUser();
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
            }
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
            CreatedDate = dto.CreatedDate
        };
        return card;
    }
}
