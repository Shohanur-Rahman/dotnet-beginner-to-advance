using System.ComponentModel.DataAnnotations;

namespace JobFinderWebApp.Models
{
    public class TodoViewModel
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public required string Title { get; set; }
        public required string Details { get; set; }
    }
}
