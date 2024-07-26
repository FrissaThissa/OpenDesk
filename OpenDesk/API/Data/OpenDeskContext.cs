using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace API.Data;

public class OpenDeskContext : IdentityDbContext<IdentityUser>
{
    public OpenDeskContext(DbContextOptions<OpenDeskContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Workspace>()
            .HasMany(w => w.Boards)
            .WithOne(b => b.Workspace)
            .HasForeignKey(b => b.WorkspaceId);

        modelBuilder.Entity<Board>()
            .HasMany(b => b.Cards)
            .WithOne(c => c.Board)
            .HasForeignKey(c => c.BoardId);
    }

    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Card> Cards { get; set; }
}
