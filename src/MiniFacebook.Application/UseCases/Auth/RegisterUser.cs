using MiniFacebook.Application.Interfaces.Users;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Auth
{
    public class RegisterUser: IRegisterUser
    {
        private readonly IUserRepository _userRepository;

        public RegisterUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> ExecuteAsync(string fullName, string email, string password)
        {
            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing != null)
                throw new Exception("Email already in use");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User(fullName, email, passwordHash);

            await _userRepository.CreateAsync(user);
            return user;
        }
    }

}
