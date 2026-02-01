using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models
{
    public class RegisterViewModel
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public required string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }
    }
}
