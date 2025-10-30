using Microsoft.AspNetCore.Mvc;
using BookStore_293.Models;
using BookStore_293.Data;
using Microsoft.AspNetCore.Authorization;

namespace BookStore_293.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Show cart page
        public IActionResult Index()
        {
            var email = User.Identity?.Name;
            var items = _context.CartItems.Where(c => c.UserEmail == email).ToList();
            ViewBag.Total = items.Sum(i => i.Price * i.Quantity);
            return View(items);
        }

        // Add to cart
        [HttpPost]
        public IActionResult Add(int bookId, string title, decimal price)
        {
            var email = User.Identity?.Name;
            if (email == null)
                return RedirectToAction("Login", "Account");

            var existing = _context.CartItems.FirstOrDefault(c => c.BookId == bookId && c.UserEmail == email);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    BookId = bookId,
                    Title = title,
                    Price = price,
                    Quantity = 1,
                    UserEmail = email
                });
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Remove from cart
        public IActionResult Remove(int id)
        {
            var item = _context.CartItems.Find(id);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Mock payment (checkout)
        public IActionResult Checkout()
        {
            var email = User.Identity?.Name;
            var items = _context.CartItems.Where(c => c.UserEmail == email).ToList();

            if (!items.Any())
                return RedirectToAction("Index");

            // Simulate payment success
            foreach (var item in items)
                _context.CartItems.Remove(item);

            _context.SaveChanges();
            TempData["Message"] = "✅ Payment successful! Your order has been placed.";
            return RedirectToAction("Index", "Books");
        }
    }
}
