using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Auth;

public class ApplicationUser : IdentityUser<int>
{
    public List<WorkspaceMember> Workspaces { get; set; } = default!;
    public List<BoardMember> Boards { get; set; } = default!;
}