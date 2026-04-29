namespace fleetops_backend.Models;

public class User
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string? Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}