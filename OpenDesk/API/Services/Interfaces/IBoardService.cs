using API.Models;
using Shared.Models;

namespace API.Services.Interfaces;

public interface IBoardService
{
    Task<IEnumerable<BoardDto>> GetAllBoardsDtoAsync(ApplicationUser user);
    Task<BoardDto?> GetBoardDtoByIdAsync(int id, ApplicationUser user);
    Task<IEnumerable<BoardDto?>> GetBoardsDtoByWorkspaceIdAsync(int id, ApplicationUser user);
    Task CreateBoardAsync(BoardDto board, ApplicationUser user);
    Task EditBoardAsync(BoardDto board, ApplicationUser user);
    Task DeleteBoardAsync(int id, ApplicationUser user);
}
