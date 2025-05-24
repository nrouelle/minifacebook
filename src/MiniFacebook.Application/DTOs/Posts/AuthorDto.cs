namespace MiniFacebook.Application.DTOs.Posts
{
    public class AuthorDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public AuthorDto(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}