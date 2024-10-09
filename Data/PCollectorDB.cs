using FirstMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMVC.Data
{
    public class PCollectorDB : DbContext
    {
        public PCollectorDB(DbContextOptions<PCollectorDB> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        // Relaties configureren tussen modellen
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Collection to Inventory (one-to-many)
            modelBuilder.Entity<Collection>()
                .HasOne(c => c.Inventory)
                .WithMany(i => i.Collections)
                .HasForeignKey(c => c.InventoryID);

            // Card to Collection (many-to-many)
            modelBuilder.Entity<Card>()
                .HasMany(c => c.Collections)
                .WithMany(c => c.Cards);
        }
    }
}