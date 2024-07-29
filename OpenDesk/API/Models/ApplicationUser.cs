using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class ApplicationUser : IdentityUser<int>
{
    public List<Workspace> Workspaces { get; set; } = default!;
    public List<Board> Boards { get; set; } = default!;
    public List<Card> Cards { get; set; } = default!;
}
