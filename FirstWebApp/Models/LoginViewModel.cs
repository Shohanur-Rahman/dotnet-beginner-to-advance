using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models
{
    public class LoginViewModel
    {
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
