using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMVC.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Card> Cards { get; set; } = new List<Card>();


    }
}
