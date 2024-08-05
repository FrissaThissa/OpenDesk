using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Frontend.Pages.Board;

public partial class Index
{
    [Parameter]
    public int WorkspaceId { get; set; }

    [Inject] public BoardService BoardService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private IEnumerable<BoardDto>? boards = default!;

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            boards = await BoardService.GetBoardsByWorkspaceIdAsync(WorkspaceId);
        }
        catch (HttpRequestException ex) 
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                NavigationManager.NavigateTo("/login");
            Console.WriteLine(ex);
        }
    }

    private void NavigateToCards(int boardId)
    {
        NavigationManager.NavigateTo($"/cards/{boardId}");
    }

    private void CreateBoard()
    {
        NavigationManager.NavigateTo($"/board/add/{WorkspaceId}");
    }

    private void EditBoard(int boardId)
    {
        NavigationManager.NavigateTo($"/board/edit/{boardId}");
    }

    private void DeleteBoard(int boardId)
    {
        NavigationManager.NavigateTo($"/board/delete/{boardId}");
    }
}