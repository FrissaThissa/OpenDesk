using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Workspace
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Board> Boards { get; set; } = default!;
    public int OwnerId { get; set; }
    public ApplicationUser Owner { get; set; } = default!;
}
