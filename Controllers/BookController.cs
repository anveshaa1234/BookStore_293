using Microsoft.AspNetCore.Mvc;
using BookStore_293.Models;

namespace BookStore_293.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index(string searchTerm)
        {
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "The Silent Patient", Author = "Alex Michaelides", Price = 499, Edition = "1st", Genre = "Thriller", Published = "Date(2019, 2, 5)", ImageUrl = "/images/silent.jpg" },
                new Book { Id = 2, Title = "It Ends With Us", Author = "Colleen Hoover", Price = 399, Edition = "2nd", Genre = "Romance", Published = "Date(2016, 8, 2)", ImageUrl = "/images/endswithus.jpg" },
                new Book { Id = 3, Title = "Atomic Habits", Author = "James Clear", Price = 450, Edition = "1st", Genre = "Self-help", Published = "Date(2018, 10, 16)", ImageUrl = "/images/atomic.jpg" },
                new Book { Id = 4, Title = "Verity", Author = "Colleen Hoover", Price = 420, Edition = "1st", Genre = "Romantic Thriller", Published =" Date(2021, 3, 10)", ImageUrl = "/images/verity.jpg" },
                new Book { Id = 5, Title = "The Psychology of Money", Author = "Morgan Housel", Price = 350, Edition = "1st", Genre = "Finance", Published = "Date(2020, 9, 8)", ImageUrl = "/images/money.jpg" },
                new Book { Id = 6, Title = "Shatter Me", Author = "Tahereh Mafi", Price = 280, Edition = "1st", Genre = "Fiction", Published = "Date(2016, 04, 21)", ImageUrl = "/images/shatter.jpg" },
                new Book { Id = 7, Title = "Fourth Wing", Author = "Rebecca Yarros", Price = 300, Edition = "1st", Genre = "Fantasy", Published = "Date(2022, 06, 5)", ImageUrl = "/images/fourth.jpg" },
                new Book { Id = 8, Title = "AGGTM", Author = "Holly Jackson", Price = 190, Edition = "1st", Genre = "Crime Thriller", Published = "Date(2015, 07, 3)", ImageUrl = "/images/AGGTM.jpg" },
                new Book { Id = 9, Title = "Ikigai", Author = "Jacon MOrgan", Price = 210, Edition = "1st", Genre = "Self help", Published = "Date(2020, 9, 8)", ImageUrl = "/images/ikigai.jpg" },
                new Book { Id = 10, Title = "Harry Potter", Author = "J.K Rowling", Price = 320, Edition = "1st", Genre = "Fantasy", Published = "Date(2001, 9, 15)", ImageUrl = "/images/harry.jpg" }
            };

            // ✅ Filter books if search term is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = books
                    .Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                             || b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(books);
        }
    }
}
