using Shared.Models;

namespace Frontend.Services;

public class CardService
{
    private readonly ApiService _apiService;

    public CardService(ApiService apiService)
    {    
        _apiService = apiService; 
    }

    public async Task<IEnumerable<CardDto>?> GetCardsByBoardId(int id)
    {
        return await _apiService.GetAsync<IEnumerable<CardDto>>($"Card/board/{id}");
    }

    public async Task<CardDto?> CreateBoardAsync(CardDto card)
    {
        return await _apiService.PostAsync<CardDto>("Card", card);
    }

    public async Task<CardDto?> EditBoardAsync(CardDto card)
    {
        return await _apiService.PutAsync<CardDto>("Card", card);
    }

    public async Task<bool> DeleteBoardAsync(int id)
    {
        return await _apiService.DeleteAsync($"Card/{id}");
    }
}
