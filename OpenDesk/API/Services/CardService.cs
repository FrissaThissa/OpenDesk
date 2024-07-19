using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace API.Services;

public class CardService : ICardService
{
    private readonly OpenDeskContext _context;

    public CardService(OpenDeskContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Card>> GetAllCardsAsync()
    {
        return await _context.Cards.ToListAsync();
    }

    public async Task<Card?> GetCardByIdAsync(int id)
    {
        return await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Card>> GetCardsByBoardId(int id)
    {
        return await _context.Cards.Where(c => c.BoardId == id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Card>> GetCardsByWorkspaceId(int id)
    {
        return await _context.Cards.Where(c => c.Board.WorkspaceId == id)
            .ToListAsync();
    }
}
