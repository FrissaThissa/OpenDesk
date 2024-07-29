using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data;

public static class SeedData
{
    public static async void Initialize(IServiceProvider serviceProvider)
    {
        using OpenDeskContext context = new OpenDeskContext(serviceProvider.GetRequiredService<DbContextOptions<OpenDeskContext>>());

        if(context.Workspaces != null && !context.Workspaces.Any())
        {
            Workspace workspace = new() { 
                Name = "Test workspace" 
            };
            context.Workspaces.Add(workspace);

            Board board1 = new()
            {
                Name = "Test board 1",
                Workspace = workspace
            };
            context.Boards.Add(board1);
            Board board2 = new()
            {
                Name = "Test board 2",
                Workspace = workspace
            };
            context.Boards.Add(board2);

            Card card1 = new()
            {
                Title = "Test card 1",
                Description = "This is test card 1 on board 1",
                CreatedDate = DateTime.Now,
                Board = board1
            };
            context.Cards.Add(card1);
            Card card2 = new()
            {
                Title = "Test card 2",
                Description = "This is test card 2 on board 1",
                CreatedDate = DateTime.Now,
                Board = board1
            };
            context.Cards.Add(card2);
            Card card3 = new()
            {
                Title = "Test card 3",
                Description = "This is test card 3 on board 1",
                CreatedDate = DateTime.Now,
                Board = board1
            };
            context.Cards.Add(card3);

            Card card4 = new()
            {
                Title = "Test card 4",
                Description = "This is test card 1 on board 2",
                CreatedDate = DateTime.Now,
                Board = board2
            };
            context.Cards.Add(card4);
            Card card5 = new()
            {
                Title = "Test card 5",
                Description = "This is test card 2 on board 2",
                CreatedDate = DateTime.Now,
                Board = board2
            };
            context.Cards.Add(card5);
            Card card6 = new()
            {
                Title = "Test card 6",
                Description = "This is test card 3 on board 2",
                CreatedDate = DateTime.Now,
                Board = board2
            };
            context.Cards.Add(card6);

            await context.SaveChangesAsync();
        }
    }
}
