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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=ApplicationDbContextA2;Integrated Security=true;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Collection to Category (one-to-many)
            modelBuilder.Entity<Collection>()
                .HasOne(c => c.Inventory)
                .WithMany(c => c.Collections)
                .HasForeignKey(c => c.InventoryID);

            // Category to Item (many-to-many)
            modelBuilder.Entity<Card>()
                .HasMany(c => c.Collections)
                .WithMany(c => c.Cards);
        }
    }
}
            


    
