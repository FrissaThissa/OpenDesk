using Shared.Models;

namespace API.Services.Interfaces;

public interface ICardService
{
    Task<IEnumerable<CardDto>?> GetAllCardsDtoAsync();
    Task<CardDto?> GetCardDtoByIdAsync(int id);
    Task<IEnumerable<CardDto>?> GetCardsDtoByBoardIdAsync(int id);
    Task<IEnumerable<CardDto>?> GetCardsDtoByWorkspaceIdAsync(int id);
    Task<CardDto?> CreateCardAsync(CardDto card);
    Task<CardDto?> EditCardAsync(CardDto card);
    Task DeleteCardAsync(int id);
}
