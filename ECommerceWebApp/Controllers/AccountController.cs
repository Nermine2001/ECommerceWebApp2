using ECommerceWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceWebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AccountController(ApplicationDbContext context)
		{
			_context = context;
		}

		// Login View
		public IActionResult Login()
		{
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
            ViewData["CartItemsCount"] = userId != null
                ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
                : 0;
            return View();
		}

        // Handle Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);
					HttpContext.Session.SetString("UserId", user.Id);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }

            return View(model);
        }


        // Register View
        public IActionResult Register()
		{
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
            ViewData["CartItemsCount"] = userId != null
                ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
                : 0;
            return View();
		}

        // Handle Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Vérification si l'utilisateur existe
                var existingUser = await _context.Users.AnyAsync(u => u.Email == model.Email);
                if (existingUser)
                {
                    ModelState.AddModelError(string.Empty, "User already exists");
                    return View(model);
                }

                // Création de l'utilisateur avec hachage du mot de passe
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Password = model.Password,
                    Role = "Customer" // Rôle par défaut
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);


                // Connexion automatique
                HttpContext.Session.SetString("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.Name);

                // Redirection
                return RedirectToAction("Index", "Home");
            }

            // Retourner la vue avec les erreurs
            return View(model);
        }


        // Logout
        [HttpPost]
		public IActionResult Logout()
		{
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;
            ViewData["CartItemsCount"] = userId != null
                ? _context.CartItems.Where(ci => ci.UserId == userId).Count()
                : 0;
            HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}

        // Profile View
        [Authorize]
        public IActionResult Profile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            ViewData["CartItemsCount"] = _context.CartItems.Where(ci => ci.UserId == userId).Count();

            return View(user);
        }


        // Update Profile
        [Authorize]
        [HttpPost]
		
		public async Task<IActionResult> Profile(User user)
		{
			if (ModelState.IsValid)
			{
				var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);

				if (existingUser != null)
				{
					existingUser.Name = user.Name;
					existingUser.Email = user.Email;

					_context.Users.Update(existingUser);
					await _context.SaveChangesAsync();

					return RedirectToAction("Profile");
				}
			}

			return View(user);
		}
	}
}
