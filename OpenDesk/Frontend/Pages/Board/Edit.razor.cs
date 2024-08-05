using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages.Board;

public partial class Edit
{
    [Inject] public BoardService BoardService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public int BoardId { get; set; }

    private BoardDto? boardDto { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        boardDto = await BoardService.GetBoardById(BoardId);
    }

    private async void PutBoard()
    {
        if (boardDto == null)
            return;

        try
        {
            await BoardService.EditBoardAsync(boardDto);
            NavigationManager.NavigateTo($"/boards/{boardDto.WorkspaceId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
