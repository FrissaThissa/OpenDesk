using Shared.Models;

namespace Frontend.Services;

public class BoardService
{
    private readonly ApiService _apiService;

    public BoardService(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<BoardDto?> GetBoardById(int id)
    {
        return await _apiService.GetAsync<BoardDto>($"Board/{id}");
    }

    public async Task<IEnumerable<BoardDto>?> GetBoardsByWorkspaceIdAsync(int id)
    {
        return await _apiService.GetAsync<IEnumerable<BoardDto>>($"Board/workspace/{id}");
    }

    public async Task<BoardDto?> CreateBoardAsync(BoardDto board)
    {
        return await _apiService.PostAsync<BoardDto>("Board", board);
    }

    public async Task<BoardDto?> EditBoardAsync(BoardDto board)
    {
        return await _apiService.PutAsync<BoardDto>("Board", board);
    }

    public async Task<bool> DeleteBoardAsync(BoardDto board)
    {
        return await _apiService.DeleteAsync($"Board/{board.Id}");
    }
}
