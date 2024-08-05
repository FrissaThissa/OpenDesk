using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages.Card;

public partial class Index
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

    private void CreateCard()
    {
        NavigationManager.NavigateTo($"/card/add/{BoardId}");
    }

    private void EditCard(int cardId)
    {
        NavigationManager.NavigateTo($"/card/edit/{cardId}");
    }

    private void DeleteCard(int cardId)
    {
        NavigationManager.NavigateTo($"/card/delete/{cardId}");
    }
}
