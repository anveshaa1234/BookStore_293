using System.ComponentModel.DataAnnotations;

namespace BookStore_293.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string UserName { get; set; } = "";

        [Required, MaxLength(200)]
        public string Email { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";
    }
}
