using Shared.Models;

namespace API.Services.Interfaces;

public interface IBoardService
{
    Task<IEnumerable<BoardDto>?> GetAllBoardsDtoAsync();
    Task<BoardDto?> GetBoardDtoByIdAsync(int id);
    Task<IEnumerable<BoardDto>?> GetBoardsDtoByWorkspaceIdAsync(int id);
    Task<BoardDto?> CreateBoardAsync(BoardDto board);
    Task<BoardDto?> EditBoardAsync(BoardDto board);
    Task DeleteBoardAsync(int id);
}
