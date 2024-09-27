using System.Collections.Generic;
using System.Security.Policy;

namespace FirstMVC.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Foreign key naar Inventory (veel-op-een-relatie)
        public int InventoryID { get; set; }
        public virtual Inventory? Inventory { get; set; } = null!;
        
        public List <Card> Cards { get; set; } = new List<Card> ();
    }
}
