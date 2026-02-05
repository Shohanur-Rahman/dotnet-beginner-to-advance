namespace JobFinderWebApp.Models
{
    public class DbTodoItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
    }
}
