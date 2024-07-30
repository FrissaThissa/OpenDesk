namespace Shared.Models;

public class BoardDto
{
    public int Id { get; set; }
    public int WorkspaceId { get; set; }
    public WorkspaceDto Workspace { get; set; } = default!;
    public string? Name { get; set; }
    public List<CardDto> Cards { get; set; } = default!;
    public UserDto CreatedBy { get; set; } = default!;
}
