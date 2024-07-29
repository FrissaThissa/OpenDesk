using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Shared.Models;

namespace API.Services;

public class CardService : ICardService
{
    private readonly OpenDeskContext _context;

    public CardService(OpenDeskContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CardDto>> GetAllCardsDtoAsync(ApplicationUser user)
    {
        List<Card> cards = await _context.Cards.Where(c => c.OwnerId == user.Id)
            .ToListAsync();

        return ConvertListToDto(cards);
    }

    public async Task<CardDto?> GetCardDtoByIdAsync(int id, ApplicationUser user)
    {
        Card? card = await _context.Cards.Where(c => c.OwnerId == user.Id)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (card == null)
            return null;

        return ConvertObjectToDto(card);
    }

    public async Task<IEnumerable<CardDto>> GetCardsDtoByBoardIdAsync(int id, ApplicationUser user)
    {
        List<Card> cards =  await _context.Cards.Where(c => c.BoardId == id)
            .Where(c => c.OwnerId == user.Id)
            .ToListAsync();

        return ConvertListToDto(cards);
    }

    public async Task<IEnumerable<CardDto>> GetCardsDtoByWorkspaceIdAsync(int id, ApplicationUser user)
    {
        List<Card> cards =  await _context.Cards.Where(c => c.Board.WorkspaceId == id)
            .Where(c => c.OwnerId == user.Id)
            .ToListAsync();

        return ConvertListToDto(cards);
    }

    public async Task CreateCardAsync(CardDto dto, ApplicationUser user)
    {
        Card card = ConvertDtoToObject(dto);

        card.Owner = user;

        _context.Cards.Add(card);
        await _context.SaveChangesAsync();
    }

    public async Task EditCardAsync(CardDto dto, ApplicationUser user)
    {
        Card card = ConvertDtoToObject(dto);

        Card? dbCard = await GetCardByIdAsync(card.Id, user);
        if (dbCard == null)
            return;

        _context.Cards.Update(card);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCardAsync(int id, ApplicationUser user)
    {
        Card? card = await GetCardByIdAsync(id, user);
        if (card == null)
            return;

        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();
    }

    private async Task<Card?> GetCardByIdAsync(int id, ApplicationUser user)
    {
        return await _context.Cards.Where(c => c.OwnerId == user.Id)
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
            OwnerId = card.Owner.Id,
            OwnerEmail = card.Owner.Email
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
            OwnerId = dto.OwnerId
        };
        return card;
    }
}
