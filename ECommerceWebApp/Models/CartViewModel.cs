using ECommerceWebApp.Models;

public class CartViewModel
{
    public List<CartItemViewModel> Items { get; set; }
    public decimal Total { get; set; }
}