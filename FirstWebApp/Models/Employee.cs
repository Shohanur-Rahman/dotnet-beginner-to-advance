using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebApp.Models
{
    /// <summary>
    /// This class will represent database entity for Employee
    /// </summary>
    [Index(nameof(Email), IsUnique =true)]
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        /// <remarks>Name must be between 3 to 100 charaters.</remarks>
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength =3, ErrorMessage ="Name must be between 3 to 100 charaters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can contain only letters.")]
        [Display(Name = "Employee Name")]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the email address associated with the user.
        /// </summary>
        /// <remarks>Valid email format is required.</remarks>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name of the department.
        /// <remarks>Department must be between 2 to 50 charaters</remarks>
        [Required(ErrorMessage = "Deparment is required")]
        [StringLength(50, MinimumLength =2, ErrorMessage ="Department must be between 2 to 50 charaters")]
        public string Department { get; set; }= default!;

        /// <summary>
        /// Gets or sets the age of the individual.
        /// </summary>
        /// <remarks>The valid range for this property is 18 to 65, inclusive. Assigning a value outside
        /// this range will result in a validation error.</remarks>
        [Required(ErrorMessage = "Age is required")]
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the salary amount for the employee.
        /// </summary>
        /// <remarks>The salary must be between 1,000 and 500,000 inclusive. Values outside this range
        /// will result in a validation error.</remarks>
        [Required(ErrorMessage = "Salary is required")]
        [Range(1000, 500000, ErrorMessage = "Salary must be between 1,000 and 500,000")]
        public decimal Salary { get; set; }
        public string? PhotoPath { get; set; }
        [NotMapped]
        [Display(Name = "Upload Photo")]
        public IFormFile? Attachment { get; set; }
    }
}
