namespace MiniFacebook.Application.Interfaces.Users
{
    public interface ICheckUserExists
    {
        Task<bool> ExecuteAsync(string authorEmail);
    }
}
