using Connect4GameApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Connect4GameApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Game>? Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Host)
                .WithMany(u => u.HostGames)
                .HasForeignKey(g => g.HostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Guest)
                .WithMany(u => u.GuestGames)
                .HasForeignKey(g => g.GuestId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
