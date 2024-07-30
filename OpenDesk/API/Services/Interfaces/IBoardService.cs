using Shared.Models;

namespace API.Services.Interfaces;

public interface IBoardService
{
    Task<IEnumerable<BoardDto>?> GetAllBoardsDtoAsync();
    Task<BoardDto?> GetBoardDtoByIdAsync(int id);
    Task<IEnumerable<BoardDto>?> GetBoardsDtoByWorkspaceIdAsync(int id);
    Task CreateBoardAsync(BoardDto board);
    Task EditBoardAsync(BoardDto board);
    Task DeleteBoardAsync(int id);
}
