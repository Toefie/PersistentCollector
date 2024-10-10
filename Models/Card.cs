using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstMVC.Models
{
    public class Card
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string? Description { get; set; }
        [Range(1, 10, ErrorMessage = "Psa moet tussen de 1 en 10 liggen.")]
        public int Psa { get; set; }
        public DateTime BuyDate { get; set; }
        public int CurrentPrice { get; set; }
        public string Specialty { get; set; }
        public int CollectionId { get; set; }
       

        // Navigatie-eigenschap naar de CardCollection (veel-op-veel-relatie)

        public virtual ICollection<Collection> Collections { get; set; }
    }
}
