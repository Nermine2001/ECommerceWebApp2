namespace ECommerceWebApp.Models
{
    public class CartItemViewModel
    {
		public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
