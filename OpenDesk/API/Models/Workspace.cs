using System.ComponentModel.DataAnnotations;
using API.Models.Auth;

namespace API.Models;

public class Workspace
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Board> Boards { get; set; } = default!;
    public int CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; } = default!;
    public List<WorkspaceMember> Members { get; set; } = default!;
}
