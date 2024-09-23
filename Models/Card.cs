using System.ComponentModel.DataAnnotations.Schema;

namespace FirstMVC.Models

{
    public class Card
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Psa { get; set; }
        public DateTime BuyDate { get; set; }
        public int CurrentPrice { get; set; }
        public string Specialty { get; set; }


        // Foreign key naar de Collection
        public int CollectionId { get; set; }

        // Navigatie-eigenschap naar de collectie
        public virtual Collection? Collection { get; set; } = null!;

    }
}
