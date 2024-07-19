using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Card
{
    public int Id { get; set; }
    public int BoardId { get; set; }
    public Board Board { get; set; } = default!;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
}
