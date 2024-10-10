namespace FirstMVC.Models
{
    public class CardViewModel
    {
        public Card? Card { get; set; }
        public decimal TotalPrice => Card.Price;
    }
}
