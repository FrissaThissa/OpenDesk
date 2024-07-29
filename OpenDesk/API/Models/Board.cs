using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Board
{
    [Key]
    public int Id { get; set; }
    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; } = default!;
    public string? Name { get; set; }
    public List<Card> Cards { get; set; } = default!;
    public int OwnerId { get; set; }
    public ApplicationUser Owner { get; set; } = default!;
}
