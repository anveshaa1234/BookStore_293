using Microsoft.AspNetCore.Mvc;
using BookStore_293.Data;
using BookStore_293.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace BookStore_293.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db) { _db = db; }

        private string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "All fields are required.";
                return View();
            }

            if (await _db.Users.AnyAsync(u => u.Email == email))
            {
                ViewBag.Error = "Email already registered.";
                return View();
            }

            var user = new AppUser { UserName = username, Email = email, PasswordHash = Hash(password) };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            // auto-login
            await SignInUser(user);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(AppUser user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            var hash = Hash(password ?? "");
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == hash);
            if (user == null)
            {
                ViewBag.Error = "Invalid credentials.";
                return View();
            }
            await SignInUser(user);
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login");
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _db.Users.FindAsync(id);
            return View(user);
        }
    }
}
