using API.Models;
using Shared.Models;

namespace API.Services.Interfaces;

public interface ICardService
{
    Task<IEnumerable<CardDto>> GetAllCardsDtoAsync(ApplicationUser user);
    Task<CardDto?> GetCardDtoByIdAsync(int id, ApplicationUser user);
    Task<IEnumerable<CardDto>> GetCardsDtoByBoardIdAsync(int id, ApplicationUser user);
    Task<IEnumerable<CardDto>> GetCardsDtoByWorkspaceIdAsync(int id, ApplicationUser user);
    Task CreateCardAsync(CardDto card, ApplicationUser user);
    Task EditCardAsync(CardDto card, ApplicationUser user);
    Task DeleteCardAsync(int id, ApplicationUser user);
}
