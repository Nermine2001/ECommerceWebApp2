using ECommerceWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
            ViewData["CartItemsCount"] = userId != null
                ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
                : 0;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContact(Contact contact)
        {
            if (ModelState.IsValid) 
            {
                var cont = new Contact
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Email = contact.Email,
                    Message = contact.Message,
                };
                _context.Contacts.Add(cont);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction("Index");
            }
            return View(contact);
        }
    }
}
