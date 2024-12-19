using Microsoft.AspNetCore.Mvc;
using ECommerceWebApp.Models;
using System.Linq;
using ECommerceWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
            var orders = _context.Orders.Where(o=> o.UserId==userId).OrderByDescending(o=>o.OrderDate).ToList();
            ViewData["CartItemsCount"] = userId != null
                ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
                : 0;
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
            ViewData["CartItemsCount"] = userId != null
                ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
                : 0;
            var order = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefault(o=>o.Id==id);
            if (order == null || order.UserId != userId)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        public IActionResult Create(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Votre panier est vide.");
                return View("Checkout", model);
            }

            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;

            // Map BillingDetails from the view model to the entity
            var billingDetails = new BillingDetails
            {
                FirstName = model.BillingDetails.FirstName,
                LastName = model.BillingDetails.LastName,
                Address = model.BillingDetails.Address,
                City = model.BillingDetails.City,
                Country = model.BillingDetails.Country,
                Postcode = model.BillingDetails.Postcode,
                Mobile = model.BillingDetails.Mobile,
                Email = model.BillingDetails.Email
            };

            // Create a new order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                BillingDetails = billingDetails, // Associate the billing details
                OrderItems = _context.CartItems
                    .Where(ci => ci.UserId == userId)
                    .Select(ci => new OrderItem
                    {
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        Price = ci.Product.Price
                    }).ToList()
            };

            // Save the order and billing details
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Clear the cart
            var cartItems = _context.CartItems.Where(ci => ci.UserId == userId);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
            ViewData["CartItemsCount"] = _context.CartItems.Count(ci => ci.UserId == userId);

            // Redirect to the order confirmation page
            return RedirectToAction("Confirm", new { id = order.Id });
        }



        public IActionResult Confirm(int id)
        {
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
            var order = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefault(o=>o.Id==id);
            if(order==null || order.UserId != userId)
            {
                return NotFound();
            }
            return View(order);
        }

		public IActionResult Checkout()
		{
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
            if (userId == null || !_context.CartItems.Any(ci => ci.UserId == userId))
            {
                return RedirectToAction("Index", "Cart");
            }

            ViewData["CartItemsCount"] = userId != null
                ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
                : 0;
            

			// Example: Fetch cart items from the database
			var cartItems = _context.CartItems.Where(ci => ci.UserId == userId)
                                         .Include(ci => ci.Product)
										 .Select(ci => new 
										 {
											 ProductName = ci.Product.Name,
											 Price = ci.Product.Price,
											 Quantity = ci.Quantity
										 }).ToList();

			var subtotal = cartItems.Sum(i => i.Price * i.Quantity);
			var shippingCost = 15.00m; // Example shipping cost
			var total = subtotal + shippingCost;

			var viewModel = new CheckoutViewModel
			{
				CartItems = cartItems.Select(i => new CartItemViewModel
				{
					ProductName = i.ProductName,
					Price = i.Price,
					Quantity = i.Quantity
				}).ToList(),
				Subtotal = subtotal,
				ShippingCost = shippingCost,
				Total = total,
				BillingDetails = new BillingDetails() // Optional: Pre-fill user details
			};

			return View(viewModel);
		}
	}
}
