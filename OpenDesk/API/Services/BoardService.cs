using API.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Shared.Models;
using API.Models.Auth;
using API.Extensions;

namespace API.Services;

public class BoardService : IBoardService
{
    private readonly OpenDeskContext _context;
    private readonly IUserService _userService;

    public BoardService(OpenDeskContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<IEnumerable<BoardDto>?> GetAllBoardsDtoAsync()
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        List<Board> boards = await _context.Boards.Include(b => b.CreatedBy)
            .Include(b => b.Cards)
            .Where(b => b.Members.Any(m => m.UserId == user.Id))
            .ToListAsync();

        return ConvertListToDto(boards);
    }

    public async Task<BoardDto?> GetBoardDtoByIdAsync(int id)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        Board? board = await _context.Boards.Include(b => b.CreatedBy)
            .Include(b => b.Cards)
            .Where(b => b.Members.Any(m => m.UserId == user.Id))
            .FirstOrDefaultAsync(b => b.Id == id);

        if (board == null)
            return null;

        return ConvertObjectToDto(board);
    }

    public async Task<IEnumerable<BoardDto>?> GetBoardsDtoByWorkspaceIdAsync(int id)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        List<Board> boards = await _context.Boards.Include(b => b.CreatedBy)
            .Where(b => b.WorkspaceId == id)
            .Where(b => b.Members.Any(m => m.UserId == user.Id))
            .ToListAsync();

        return ConvertListToDto(boards);
    }

    public async Task<BoardDto?> CreateBoardAsync(BoardDto dto)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        Board board = ConvertDtoToObject(dto);
        board.CreatedBy = user;
        _context.Boards.Add(board);

        BoardMember member = new BoardMember()
        {
            Board = board,
            User = user
        };
        _context.BoardMembers.Add(member);

        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task<BoardDto?> EditBoardAsync(BoardDto dto)
    {
        Board? board = await GetBoardByIdAsync(dto.Id);
        if (board == null)
            return null;

        board.Name = dto.Name;

        _context.Boards.Update(board);
        await _context.SaveChangesAsync();
        return dto;
    }

    public async Task DeleteBoardAsync(int id)
    {
        Board? board = await GetBoardByIdAsync(id);
        if (board == null)
            return;

        _context.Boards.Remove(board);
        await _context.SaveChangesAsync();
    }

    private async Task<Board?> GetBoardByIdAsync(int id)
    {
        ApplicationUser? user = await _userService.GetCurrentUser();
        if (user == null)
            return null;

        return await _context.Boards.Include(b => b.CreatedBy)
            .Include(b => b.Cards)
            .Where(b => b.Members.Any(m => m.UserId == user.Id))
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    private BoardDto ConvertObjectToDto(Board board)
    {
        BoardDto dto = new BoardDto()
        {
            Id = board.Id,
            Name = board.Name,
            CreatedBy = new UserDto()
            {
                Id = board.CreatedBy.Id,
                Name = board.CreatedBy.UserName,
                Email = board.CreatedBy.Email
            },
            WorkspaceId = board.WorkspaceId
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
            WorkspaceId = dto.WorkspaceId
        };
        return board;
    }
}
