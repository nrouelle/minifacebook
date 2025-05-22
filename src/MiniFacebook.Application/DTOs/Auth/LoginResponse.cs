namespace MiniFacebook.Application.DTOs.Auth
{
    public class LoginResponse
    {
        public Guid UserId { get; internal set; }
        public string FullName { get; internal set; }
        public string Email { get; internal set; }
        public string Token { get; internal set; }
    }
}
