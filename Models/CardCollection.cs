namespace FirstMVC.Models
{
    public class CardCollection
    {
        public int CardId { get; set; }
        public Card? Card { get; set; } = null!;

        public int CollectionId { get; set; }
   
        public Collection? Collection { get; set; } = null!;
    }
}



