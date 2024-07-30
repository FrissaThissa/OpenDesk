using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages;

public partial class Cards
{
    [Parameter]
    public int BoardId { get; set; }

    [Inject] public CardService CardService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private IEnumerable<CardDto>? cards = default!;

    protected override async Task OnParametersSetAsync()
    {
        cards = await CardService.GetCardsByBoardId(BoardId);
    }
}
