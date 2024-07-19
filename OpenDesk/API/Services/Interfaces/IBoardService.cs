using Shared.Models;

namespace API.Services.Interfaces;

public interface IBoardService
{
    Task<IEnumerable<Board>> GetAllBoardsAsync();
    Task<Board?> GetBoardByIdAsync(int id);
    Task<IEnumerable<Board?>> GetBoardsByWorkspaceId(int id);
}
