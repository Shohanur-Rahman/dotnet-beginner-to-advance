namespace JobFinderWebApp.Models
{
    public class DbTodoItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Details { get; set; }
    }
}
