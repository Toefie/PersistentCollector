namespace FirstMVC.Models
{
    public class CollectionCardView
    
        {
            public Card Card { get; set; }
            public List<Collection> Collections { get; set; }
            public decimal TotalPrice => Card.Price;
    }
    }

