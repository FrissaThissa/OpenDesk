using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Workspace
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Board> Boards { get; set; } = default!;
}
