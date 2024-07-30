using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages;

public partial class Boards
{
    [Parameter]
    public int WorkspaceId { get; set; }

    [Inject] public BoardService BoardService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager {  get; set; } = default!;

    private IEnumerable<BoardDto>? boards = default!;

    protected override async Task OnParametersSetAsync()
    {
        boards = await BoardService.GetBoardsByWorkspaceIdAsync(WorkspaceId);
    }

    private void NavigateToCards(int boardId)
    {
        NavigationManager.NavigateTo($"/cards/{boardId}");
    }
}