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
        public DbSet<CardCollection> CardCollections { get; set; }  // Voeg deze regel toe

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=ApplicationDbContextA2;Integrated Security=true;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuratie voor de veel-op-veel-relatie tussen Card en Collection
            modelBuilder.Entity<CardCollection>()
                .HasKey(cc => new { cc.CardId, cc.CollectionId });

            modelBuilder.Entity<CardCollection>()
                .HasOne(cc => cc.Card)
                .WithMany(c => c.CardCollections)
                .HasForeignKey(cc => cc.CardId);

            modelBuilder.Entity<CardCollection>()
                .HasOne(cc => cc.Collection)
                .WithMany(c => c.CardCollections)
                .HasForeignKey(cc => cc.CollectionId);
        }
    }
}