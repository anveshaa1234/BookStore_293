using Microsoft.AspNetCore.Mvc;
using BookStore_293.Models;
using System.Collections.Generic;

namespace BookStore_293.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Example: just display store info
            var store = new Store
            {
                Name = "My Bookstore",
                Location = "Mumbai",
                Address = "123 Street, Mumbai",
                Phone = "9876543210",
                Email = "info@bookstore.com",
                Hours = "9AM - 9PM",
                Established = "2005",
                Preference = "Fiction, Non-Fiction"
            };

            store.AddBook(new Book(1, "Book 1", "Author 1", 299.99m));
            store.AddBook(new Book(2, "Book 2", "Author 2", 199.50m));

            return View(store);
        }

        public IActionResult StoreDetails()
        {
            var store = new Store
            {
                Name = "My Bookstore",
                Location = "Mumbai",
                Address = "123 Street, Mumbai",
                Phone = "9876543210",
                Email = "info@bookstore.com",
                Hours = "9AM - 9PM",
                Established = "2005",
                Preference = "Fiction, Non-Fiction"
            };

            return View(store);
        }
    }
}
