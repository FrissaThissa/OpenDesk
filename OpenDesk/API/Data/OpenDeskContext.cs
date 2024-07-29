using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class OpenDeskContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public OpenDeskContext(DbContextOptions<OpenDeskContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Workspace>()
            .HasMany(w => w.Boards)
            .WithOne(b => b.Workspace)
            .HasForeignKey(b => b.WorkspaceId);
        modelBuilder.Entity<Workspace>()
            .HasOne(w => w.Owner)
            .WithMany(u => u.Workspaces)
            .HasForeignKey(w => w.OwnerId);

        modelBuilder.Entity<Board>()
            .HasMany(b => b.Cards)
            .WithOne(c => c.Board)
            .HasForeignKey(c => c.BoardId);
        modelBuilder.Entity<Board>()
            .HasOne(w => w.Owner)
            .WithMany(u => u.Boards)
            .HasForeignKey(w => w.OwnerId);

        modelBuilder.Entity<Card>()
            .HasOne(w => w.Owner)
            .WithMany(u => u.Cards)
            .HasForeignKey(w => w.OwnerId);
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Card> Cards { get; set; }
}
