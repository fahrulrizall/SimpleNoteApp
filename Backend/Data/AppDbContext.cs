using Microsoft.EntityFrameworkCore;
using SimpleNoteApp.Models;

namespace SimpleNoteApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasOne(n => n.User)
                  .WithMany(u => u.Notes)
                  .HasForeignKey(n => n.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data
        var seedUser = new User
        {
            Id = 1,
            Username = "demo",
            Email = "demo@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("demo123"),
            CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        modelBuilder.Entity<User>().HasData(seedUser);

        modelBuilder.Entity<Note>().HasData(
            new Note
            {
                Id = 1,
                Title = "Welcome Note",
                Content = "Welcome to SimpleNoteApp! This is your first note.",
                UserId = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Note
            {
                Id = 2,
                Title = "Getting Started",
                Content = "You can create, edit, and delete notes. Only you can see your own notes!",
                UserId = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
