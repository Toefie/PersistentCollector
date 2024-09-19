using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMVC.Models
{
    public class Inventory
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;

        // Relatie: één inventory heeft meerdere collecties
        public ICollection<Collection> Collections { get; set; }
    }
}
