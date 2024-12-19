namespace ECommerceWebApp.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public User User { get; set; }
        public Cart()
        {
            
        }

        public Cart(int id, string userId, List<CartItem> cartItems)
        {
            Id = id;
            UserId = userId;
            Items = cartItems;
        }
    }
}
