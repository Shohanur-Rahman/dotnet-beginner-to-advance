namespace FirstWebApp.Models
{
    public class DbUser
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
