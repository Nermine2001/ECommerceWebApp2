
using ECommerceWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ECommerceWebApp.ViewModels;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        try
        {
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;

            var cart = _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId, Items = new List<ECommerceWebApp.Models.CartItem>() };
            }

            Console.WriteLine($"Cart Items Count: {cart.Items.Count}");
            foreach (var item in cart.Items)
            {
                Console.WriteLine($"Product: {item.Product.Name}, Quantity: {item.Quantity}");
            }

            ViewData["CartItemsCount"] = cart.Items.Count;
            return View(cart);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return View("Error");
        }
    }
    [Authorize]
    [HttpPost]
    public IActionResult AddToCart(int productId)
    {
        var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);

        // Log the values
        Console.WriteLine($"User ID: {userId}");
        Console.WriteLine($"User Name: {user?.Name}");
        Console.WriteLine($"IsAuthenticated: {User.Identity.IsAuthenticated}");
        Console.WriteLine($"AuthenticationType: {User.Identity.AuthenticationType}");
        Console.WriteLine($"Claims: {string.Join(", ", User.Claims.Select(c => c.Type + "=" + c.Value))}");

        if (user == null)
        {
            return Unauthorized();
        }

        var product = _context.Products.Find(productId);
        if (product == null) return NotFound();

        var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId) ?? new Cart { UserId = userId };
        var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem == null)
        {
            cart.Items.Add(new ECommerceWebApp.Models.CartItem { ProductId = productId, Quantity = 1, Product = product, UserId=userId });
        }
        else
        {
            cartItem.Quantity++;
        }

        if (cart.Id == 0)
        {
            _context.Carts.Add(cart);
        }

        _context.SaveChanges();
        var cartItemsCount = _context.CartItems.Where(ci => ci.UserId == userId).Count();

        return Json(new { cartItemsCount });
    }


    [Authorize]
    [HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult IncreaseQuantity(int productId)
    {
		var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
		var cart = _context.Carts.Include(c => c.Items).FirstOrDefault(c => c.UserId == userId);
        if (cart == null) return NotFound();

        var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
        if (cartItem != null)
        {
            cartItem.Quantity++;
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
    [Authorize]
    [HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult DecreaseQuantity(int productId) 
    {
		var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
		var cart = _context.Carts.Include(c => c.Items).FirstOrDefault(c => c.UserId == userId);
        if (cart == null) return NotFound();

        var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
        if (cartItem != null)
        {
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            else
            {
                cart.Items.Remove(cartItem);
            }

            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
	[Authorize]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult RemoveItem(int productId)
	{
		var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
		var cart = _context.Carts
			.Include(c => c.Items)
			.FirstOrDefault(c => c.UserId == userId);

		if (cart == null)
		{
			return NotFound();
		}

		var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
		if (cartItem != null)
		{
			cart.Items.Remove(cartItem);
			_context.SaveChanges();
		}

		return RedirectToAction("Index");
	}

   




}

