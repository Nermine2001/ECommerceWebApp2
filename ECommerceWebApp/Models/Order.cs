using ECommerceWebApp.ViewModels;

namespace ECommerceWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId {get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public User User { get; set; }
        public BillingDetails BillingDetails { get; internal set; }

        public Order()
        {
            
        }

        public Order(int id, string userId, DateTime orderDate, string shippingAddress, decimal totalPrice)
        {
            Id = id;
            UserId = userId;
            OrderDate = orderDate;
            ShippingAddress = shippingAddress;
            TotalPrice = totalPrice;
        }
    }
}
