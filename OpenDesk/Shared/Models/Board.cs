using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Board
{
    public int Id { get; set; }
    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; } = default!;
    public string? Name { get; set; }
    public List<Card> Cards { get; set; } = default!;
}
