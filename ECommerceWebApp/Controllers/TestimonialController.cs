using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebApp.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestimonialController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var testimonials = _context.Testimonials.ToList();
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
            ViewData["CartItemsCount"] = userId != null
                ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
                : 0;
            return View(testimonials);
        }
    }
}
