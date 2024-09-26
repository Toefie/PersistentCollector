using System;
using System.Collections.Generic;

namespace FirstMVC.Models
{
    public class Card
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string? Description { get; set; }
        public int Psa { get; set; }
        public DateTime BuyDate { get; set; }
        public int CurrentPrice { get; set; }
        public string Specialty { get; set; }

        // Navigatie-eigenschap naar de CardCollection (veel-op-veel-relatie)
        public ICollection<CardCollection> CardCollections { get; set; } = new List<CardCollection>();
    }
}
