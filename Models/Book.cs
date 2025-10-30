namespace BookStore_293.Models
{
    public class Book
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Edition { get; set; } = "";
        public string Genre { get; set; } = "";
        public string Published { get; set; } = "";
        public decimal Price { get; set; } = 0m;
        public string ImageUrl { get; set; } = "";

        public Book() { }

        public Book(int id, string title, string author, decimal price)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;
        }
    }
}
