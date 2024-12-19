using ECommerceWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ECommerceWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string category = "All Products")
        {
            var products = await _context.Products.Where(p => category == "All Products" || p.Category == category).ToListAsync();
            ViewData["SelectedCategory"] = category;
            ViewData["Products"] = products;
            var vegetables = await _context.Products.Where(p => p.Category == "Vegetables").ToListAsync();
            ViewData["Vegetables"] = vegetables;
            var bestsellers = await _context.Products.Where(p => p.Rating >= 4).OrderByDescending(p => p.Rating).Take(6).ToListAsync();
            ViewData["BestSellers"] = bestsellers;
            var satisfiedCustomers = await _context.Products.Where(p => p.Rating > 4).GroupBy(p => p.Id).Select(g => new { ProductId = g.Key, SatisfiedCount = g.Count() }).ToListAsync();
            var totalSatisfiedCustomers = satisfiedCustomers.Sum(sc => sc.SatisfiedCount);
            var availableProducts = await _context.Products.CountAsync();
            ViewBag.SatisfiedCustomers = totalSatisfiedCustomers;
            ViewBag.AvailableProducts = availableProducts;
            var testimonials = await _context.Testimonials.Take(5).ToListAsync();
            ViewData["Testimonials"] = testimonials;
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
            ViewData["CartItemsCount"] = userId != null
                ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
                : 0;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
