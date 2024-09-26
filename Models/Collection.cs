using System.Collections.Generic;

namespace FirstMVC.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Foreign key naar Inventory (veel-op-een-relatie)
        public int InventoryID { get; set; }
        public virtual Inventory Inventory { get; set; } = null!;

        // Navigatie-eigenschap naar de CardCollection (veel-op-veel-relatie)
        public ICollection<CardCollection> CardCollections { get; set; } = new List<CardCollection>();
    }
}
