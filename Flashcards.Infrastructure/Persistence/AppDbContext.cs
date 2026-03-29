using Flashcards.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Name).IsUnique();
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
                entity.Property(u => u.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Deck>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(200);
                entity.Property(d => d.Tags).HasColumnType("text[]");
                entity.HasOne(d => d.User)
                    .WithMany(u => u.Decks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Front).IsRequired();
                entity.Property(c => c.Back).IsRequired();

                entity.HasOne(c => c.Deck)
                    .WithMany(d => d.Cards)
                    .HasForeignKey(c => c.DeckId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Status)
                    .WithMany(s => s.Cards)
                    .HasForeignKey(c => c.StatusId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.LastRating)
                    .WithMany(r => r.Cards)
                    .HasForeignKey(c => c.LastRatingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Value).IsRequired().HasMaxLength(50);

                // Начальные данные
                entity.HasData(
                    new Status { Id = 1, Value = "New" },
                    new Status { Id = 2, Value = "Learning" },
                    new Status { Id = 3, Value = "Review" },
                    new Status { Id = 4, Value = "Mature" }
                );
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Value).IsRequired().HasMaxLength(50);

                // Начальные данные
                entity.HasData(
                    new Rating { Id = 1, Value = "Again" },
                    new Rating { Id = 2, Value = "Hard" },
                    new Rating { Id = 3, Value = "Good" },
                    new Rating { Id = 4, Value = "Easy" }
                );
            });
        }
    }
}
