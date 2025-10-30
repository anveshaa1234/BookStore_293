using System.ComponentModel.DataAnnotations;

namespace BookStore_293.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderedAt { get; set; } = DateTime.UtcNow;

        public decimal Total { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
