using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages.Board;

public partial class Add
{
    [Inject] public BoardService BoardService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public int WorkspaceId { get; set; }

    private BoardDto boardDto { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        boardDto = new BoardDto()
        {
            WorkspaceId = WorkspaceId,
        };
    }

    private async void PostBoard()
    {
        try
        {
            await BoardService.CreateBoardAsync(boardDto);
            NavigationManager.NavigateTo($"/boards/{WorkspaceId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}