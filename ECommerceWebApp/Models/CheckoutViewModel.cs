using ECommerceWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebApp.ViewModels
{
	public class CheckoutViewModel
	{
		public List<CartItemViewModel> CartItems { get; set; }
		public decimal Subtotal { get; set; }
		public decimal ShippingCost { get; set; }
		public decimal Total { get; set; }
		public BillingDetails BillingDetails { get; set; }
	}

	public class CartItem
	{
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}

	public class BillingDetails
	{
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string Country { get; set; }
		[Required]
		public string Postcode { get; set; }
		[Required]
		[Phone]
		public string Mobile { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
