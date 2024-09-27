﻿namespace FirstMVC.Models
{
    public class CardCreateEditViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int Psa { get; set; }
        public DateTime BuyDate { get; set; }
        public int CurrentPrice { get; set; }
        public string Specialty { get; set; }

        // De geselecteerde collecties
        public int[] SelectedCollections { get; set; }
    }
}
