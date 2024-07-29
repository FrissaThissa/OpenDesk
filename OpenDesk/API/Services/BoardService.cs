using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Shared.Models;

namespace API.Services;

public class BoardService : IBoardService
{
    private readonly OpenDeskContext _context;

    public BoardService(OpenDeskContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BoardDto>> GetAllBoardsDtoAsync(ApplicationUser user)
    {
        List<Board> boards = await _context.Boards.Include(b => b.Cards)
            .Where(b => b.OwnerId == user.Id)
            .ToListAsync();

        return ConvertListToDto(boards);
    }

    public async Task<BoardDto?> GetBoardDtoByIdAsync(int id, ApplicationUser user)
    {
        Board? board = await _context.Boards.Include(b => b.Cards)
            .Where(b => b.OwnerId == user.Id)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (board == null)
            return null;

        return ConvertObjectToDto(board);
    }

    public async Task<IEnumerable<BoardDto?>> GetBoardsDtoByWorkspaceIdAsync(int id, ApplicationUser user)
    {
        List<Board> boards = await _context.Boards.Where(b => b.WorkspaceId == id)
            .Where(b => b.OwnerId == user.Id)
            .ToListAsync();

        return ConvertListToDto(boards);
    }

    public async Task CreateBoardAsync(BoardDto dto, ApplicationUser user)
    {
        Board board = ConvertDtoToObject(dto);

        board.Owner = user;

        _context.Boards.Add(board);
        await _context.SaveChangesAsync();
    }

    public async Task EditBoardAsync(BoardDto dto, ApplicationUser user)
    {
        Board board = ConvertDtoToObject(dto);

        Board? dbBoard = await GetBoardByIdAsync(board.Id, user);
        if (dbBoard == null)
            return;

        _context.Boards.Update(board);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBoardAsync(int id, ApplicationUser user)
    {
        Board? board = await GetBoardByIdAsync(id, user);
        if (board == null)
            return;

        _context.Boards.Remove(board);
        await _context.SaveChangesAsync();
    }

    private async Task<Board?> GetBoardByIdAsync(int id, ApplicationUser user)
    {
        return await _context.Boards.Include(b => b.Cards)
            .Where(b => b.OwnerId == user.Id)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    private BoardDto ConvertObjectToDto(Board board)
    {
        BoardDto dto = new BoardDto()
        {
            Id = board.Id,
            Name = board.Name,
            OwnerId = board.Owner.Id,
            OwnerEmail = board.Owner.Email
        };
        return dto;
    }

    private List<BoardDto> ConvertListToDto(List<Board> boards)
    {
        return boards.Select(ConvertObjectToDto).ToList();
    }

    private Board ConvertDtoToObject(BoardDto dto)
    {
        Board board = new Board()
        {
            Id = dto.Id,
            Name = dto.Name,
            OwnerId = dto.OwnerId
        };
        return board;
    }
}
