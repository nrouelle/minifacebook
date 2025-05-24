using MiniFacebook.Application.Interfaces.Users;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Users
{
    public class CheckUserExists : ICheckUserExists
    {
        private readonly IUserRepository userRepository;

        public CheckUserExists(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<bool> ExecuteAsync(string authorEmail)
        {
            var user = await userRepository.GetByEmailAsync(authorEmail);
            
            return user != null;
        }
    }
}
