using MiniFacebook.Application.DTOs.Auth;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Auth
{
    public class LoginUser
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUser(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        /// <summary>
        /// Executes the login use case: validates user credentials and generates a JWT token.
        /// </summary>
        /// <param name="dto">Login request data (email and password).</param>
        /// <returns>LoginResponse containing user info and token.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when credentials are invalid.</exception>
        public async Task<LoginResponse> ExecuteAsync(LoginRequest dto)
        {
            // Retrieve user by email
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // Generate JWT
            var token = _jwtTokenGenerator.GenerateToken(user);

            // Build response
            return new LoginResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Token = token
            };
        }
    }
}
