namespace MiniFacebook.Domain.Entities;

public class User
{
    public User(string fullName, string email, string passwordHash)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
    }

    internal User(string fullName, string email, string passwordHash, DateTime createdAt)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    public string FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}
