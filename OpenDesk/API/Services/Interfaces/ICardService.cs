using Shared.Models;

namespace API.Services.Interfaces;

public interface ICardService
{
    Task<IEnumerable<CardDto>?> GetAllCardsDtoAsync();
    Task<CardDto?> GetCardDtoByIdAsync(int id);
    Task<IEnumerable<CardDto>?> GetCardsDtoByBoardIdAsync(int id);
    Task<IEnumerable<CardDto>?> GetCardsDtoByWorkspaceIdAsync(int id);
    Task CreateCardAsync(CardDto card);
    Task EditCardAsync(CardDto card);
    Task DeleteCardAsync(int id);
}
