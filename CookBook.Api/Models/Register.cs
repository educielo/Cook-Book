using System.ComponentModel.DataAnnotations;

namespace CookBook.Api.Models
{
    public class Register
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}