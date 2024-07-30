using System.ComponentModel.DataAnnotations;
using API.Models.Auth;

namespace API.Models;

public class Board
{
    [Key]
    public int Id { get; set; }
    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; } = default!;
    public string? Name { get; set; }
    public List<Card> Cards { get; set; } = default!;
    public int CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; } = default!;
    public List<BoardMember> Members { get; set; } = default!;
}
