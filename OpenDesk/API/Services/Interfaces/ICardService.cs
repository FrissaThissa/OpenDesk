using Shared.Models;

namespace API.Services.Interfaces;

public interface ICardService
{
    Task<IEnumerable<Card>> GetAllCardsAsync();
    Task<Card?> GetCardByIdAsync(int id);
    Task<IEnumerable<Card>> GetCardsByBoardId(int id);
    Task<IEnumerable<Card>> GetCardsByWorkspaceId(int id);
}
