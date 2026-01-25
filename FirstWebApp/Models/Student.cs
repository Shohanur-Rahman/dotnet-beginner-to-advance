using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Department { get; set; } = default!;
        public int Age { get; set; }
    }
}
