using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages.Card;

public partial class Edit
{
    [Inject] public CardService CardService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public int CardId { get; set; }

    private CardDto? cardDto { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        cardDto = await CardService.GetCardById(CardId);
    }

    private async void PutCard()
    {
        if (cardDto == null)
            return;

        try
        {
            await CardService.EditCardAsync(cardDto);
            NavigationManager.NavigateTo($"/cards/{cardDto.BoardId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
