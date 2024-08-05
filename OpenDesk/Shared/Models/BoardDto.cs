namespace Shared.Models;

public class BoardDto
{
    public int Id { get; set; }
    public int WorkspaceId { get; set; }
    public WorkspaceDto Workspace { get; set; } = new();
    public string? Name { get; set; }
    public List<CardDto> Cards { get; set; } = [];
    public UserDto CreatedBy { get; set; } = new();
}
