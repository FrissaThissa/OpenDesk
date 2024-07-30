using System.ComponentModel.DataAnnotations;

namespace API.Models.Auth;

public class WorkspaceMember
{
    [Key]
    public int Id {  get; set; }
    public int UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;
    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; } = default!;
}
