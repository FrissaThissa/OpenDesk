using Shared.Models;

namespace Frontend.Services;

public class CardService
{
    private readonly ApiService _apiService;

    public CardService(ApiService apiService)
    {    
        _apiService = apiService; 
    }

    public async Task<CardDto?> GetCardById(int id)
    {
        return await _apiService.GetAsync<CardDto>($"Card/{id}");
    }

    public async Task<IEnumerable<CardDto>?> GetCardsByBoardId(int id)
    {
        return await _apiService.GetAsync<IEnumerable<CardDto>>($"Card/board/{id}");
    }

    public async Task<CardDto?> CreateCardAsync(CardDto card)
    {
        return await _apiService.PostAsync<CardDto>("Card", card);
    }

    public async Task<CardDto?> EditCardAsync(CardDto card)
    {
        return await _apiService.PutAsync<CardDto>("Card", card);
    }

    public async Task<bool> DeleteCardAsync(CardDto card)
    {
        return await _apiService.DeleteAsync($"Card/{card.Id}");
    }
}
