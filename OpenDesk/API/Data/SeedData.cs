using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Identity;
using API.Models.Auth;

namespace API.Data;

public static class SeedData
{
    public static async void Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            using OpenDeskContext context = new OpenDeskContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<OpenDeskContext>>());
            using UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            using RoleManager<IdentityRole<int>> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            // Ensure the database is created.
            context.Database.EnsureCreated();

            // Check if the admin role exists, and create it if it doesn't.
            string[] roles = new string[] { "Admin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

            // Check if the admin user exists, and create it if it doesn't.
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";
            ApplicationUser? admin = userManager.Users.FirstOrDefault(x => x.Email == adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            if (context.Workspaces != null && !context.Workspaces.Any())
            {
                Workspace workspace = new()
                {
                    Name = "Test workspace",
                    CreatedById = admin.Id
                };
                context.Workspaces.Add(workspace);

                WorkspaceMember workspaceMember = new()
                {
                    UserId = admin.Id,
                    Workspace = workspace
                };
                context.WorkspaceMembers.Add(workspaceMember);

                Board board = new()
                {
                    Name = "Test board 1",
                    Workspace = workspace,
                    CreatedById = admin.Id
                };
                context.Boards.Add(board);

                BoardMember boardMember = new()
                {
                    UserId = admin.Id,
                    Board = board
                };
                context.BoardMembers.Add(boardMember);

                Card card1 = new()
                {
                    Title = "Test card 1",
                    Description = "This is test card 1",
                    CreatedDate = DateTime.Now,
                    Board = board,
                    CreatedById = admin.Id
                };
                context.Cards.Add(card1);
                Card card2 = new()
                {
                    Title = "Test card 2",
                    Description = "This is test card 2",
                    CreatedDate = DateTime.Now,
                    Board = board,
                    CreatedById = admin.Id
                };
                context.Cards.Add(card2);
                Card card3 = new()
                {
                    Title = "Test card 3",
                    Description = "This is test card 3",
                    CreatedDate = DateTime.Now,
                    Board = board,
                    CreatedById = admin.Id
                };
                context.Cards.Add(card3);

                await context.SaveChangesAsync();
            }
        }
    }
}
