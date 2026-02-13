using FirstWebApp.Data;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FirstWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly FirstWebAppContext _context;
        public AccountController(FirstWebAppContext context)
        {
            _context = context;
        }

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool userExists = await _context.Users.AnyAsync(u => u.Email == model.Email);
            if (userExists)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(model);
            }
            var user = new DbUser()
            {
                Name = model.Name,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if(user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            //Create Claims and Sign In Logic Here (omitted for brevity)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                });

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
