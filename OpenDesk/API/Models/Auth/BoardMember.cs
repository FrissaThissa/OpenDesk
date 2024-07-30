using System.ComponentModel.DataAnnotations;

namespace API.Models.Auth;

public class BoardMember
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;
    public int BoardId { get; set; }
    public Board Board { get; set; } = default!;
}
