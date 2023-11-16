using Microsoft.EntityFrameworkCore;
using TurboKart.Domain.Entities;

namespace TurboKart.Infrastructure.Persistence.EfContexts
{
    public class TurboKartContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TurboKartDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Bookings)
                .WithOne(b => b.Customer)
                .HasForeignKey(b => b.CustomerId)
                .HasPrincipalKey(c => c.CustomerId);
        }

    }
}
