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
        
    }
}
