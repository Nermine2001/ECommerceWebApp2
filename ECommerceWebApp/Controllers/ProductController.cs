using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ECommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;


public class ProductController : Controller
    {
    private readonly ApplicationDbContext _context;
    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        var featuredProducts = _context.Products.Where(p => p.IsFeatured).ToList();
        var categories = _context.Products.Select(p => p.Category).Distinct().ToList();
        var viewModel = new ShopViewModel
        {
            Products = products,
            FeaturedProducts = featuredProducts,
            Categories = categories
        };
        var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
        ViewData["CartItemsCount"] = userId != null
            ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
            : 0;
        return View(viewModel);
    }
    public IActionResult Details(int id)
    {
        var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
        ViewData["CartItemsCount"] = userId != null
            ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
            : 0;
        var product = _context.Products.Include(p => p.Reviews).ThenInclude(r => r.reviewReplies).FirstOrDefault(p=> p.Id == id);
        if (product == null) return NotFound();

        var viewModel = new ShopViewModel
        {
            Category = product.Category,
            Products = new List<Product> { product },
            FeaturedProducts = _context.Products.Where(p => p.IsFeatured).Include(p => p.Reviews).ToList(),
            Categories = _context.Products.Select(p => p.Category).Distinct().ToList()
        };

        return View(viewModel);
    }
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(product);
    }
    [Authorize]
    public IActionResult Edit(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Mettre à jour les propriétés du produit
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            existingProduct.ImageURL = product.ImageURL;
            existingProduct.IsFeatured = product.IsFeatured;
            existingProduct.Weight = product.Weight;
            existingProduct.Origin = product.Origin;
            existingProduct.Quality = product.Quality;
            existingProduct.CheckStatus = product.CheckStatus;
            existingProduct.MinWeight = product.MinWeight;
            existingProduct.Rating = product.Rating;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(product);
    }


}

