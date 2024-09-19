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

        // Foreign key naar de Collection
        public int CollectionId { get; set; }

        // Navigatie-eigenschap naar de collectie
        public Collection Collection { get; set; } = null!;
    }
}
