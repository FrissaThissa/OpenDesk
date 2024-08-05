using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages.Card;

public partial class Add
{
    [Inject] public CardService CardService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public int BoardId { get; set; }

    private CardDto cardDto { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        cardDto = new CardDto() 
        { 
            BoardId = BoardId 
        };
    }

    private async void PostCard()
    {
        try
        {
            await CardService.CreateCardAsync(cardDto);
            NavigationManager.NavigateTo($"/cards/{BoardId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}