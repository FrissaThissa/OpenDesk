using API.Models;
using API.Models.Auth;
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

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Workspaces)
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId);
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Boards)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<Workspace>()
            .HasMany(w => w.Boards)
            .WithOne(b => b.Workspace)
            .HasForeignKey(b => b.WorkspaceId);
        modelBuilder.Entity<Workspace>()
            .HasOne(w => w.CreatedBy)
            .WithMany()
            .HasForeignKey(w => w.CreatedById);
        modelBuilder.Entity<Workspace>()
            .HasMany(w => w.Members)
            .WithOne(m => m.Workspace)
            .HasForeignKey(w => w.WorkspaceId);

        modelBuilder.Entity<Board>()
            .HasMany(b => b.Cards)
            .WithOne(c => c.Board)
            .HasForeignKey(c => c.BoardId);
        modelBuilder.Entity<Board>()
            .HasOne(b => b.CreatedBy)
            .WithMany()
            .HasForeignKey(b => b.CreatedById);
        modelBuilder.Entity<Board>()
            .HasMany(b => b.Members)
            .WithOne(m => m.Board)
            .HasForeignKey(b => b.BoardId);

        modelBuilder.Entity<Card>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById);
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<WorkspaceMember> WorkspaceMembers { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<BoardMember> BoardMembers { get; set; }
    public DbSet<Card> Cards { get; set; }
}
