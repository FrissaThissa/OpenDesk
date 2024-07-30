﻿namespace Shared.Models;

public class WorkspaceDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<BoardDto> Boards { get; set; } = default!;
    public UserDto CreatedBy { get; set; } = default!;
}
