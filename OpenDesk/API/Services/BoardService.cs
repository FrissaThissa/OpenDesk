using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace API.Services;

public class BoardService : IBoardService
{
    private readonly OpenDeskContext _context;

    public BoardService(OpenDeskContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Board>> GetAllBoardsAsync()
    {
        return await _context.Boards.Include(b => b.Cards)
            .ToListAsync();
    }

    public async Task<Board?> GetBoardByIdAsync(int id)
    {
        return await _context.Boards.Include(b => b.Cards)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Board?>> GetBoardsByWorkspaceId(int id)
    {
        return await _context.Boards.Where(b => b.WorkspaceId == id)
            .ToListAsync();
    }
}
