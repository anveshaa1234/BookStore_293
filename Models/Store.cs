using System.Collections.Generic;
using System.Linq;

namespace BookStore_293.Models
{
    public class Store
    {
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Hours { get; set; } = "";
        public string Established { get; set; } = "";
        public string Preference { get; set; } = "";
        public List<Book> Books { get; set; } = new List<Book>();

        public Store() { }

        public Store(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public void AddBook(Book book) => Books.Add(book);

        public bool RemoveBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                Books.Remove(book);
                return true;
            }
            return false;
        }
    }
}
