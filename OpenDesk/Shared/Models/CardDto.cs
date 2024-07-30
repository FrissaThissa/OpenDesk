namespace Shared.Models;

public class CardDto
{
    public int Id { get; set; }
    public int BoardId { get; set; }
    public BoardDto Board { get; set; } = default!;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public UserDto CreatedBy { get; set; } = default!;
}
