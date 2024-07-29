using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Card
{
    [Key]
    public int Id { get; set; }
    public int BoardId { get; set; }
    public Board Board { get; set; } = default!;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public int OwnerId { get; set; }
    public ApplicationUser Owner { get; set; } = default!;
}
