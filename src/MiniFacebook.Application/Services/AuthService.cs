using MiniFacebook.Application.DTOs;
using MiniFacebook.Domain.Interfaces;
using MiniFacebook.Domain.Entities;

namespace MiniFacebook.Application.Services;

public class AuthService
{
	private readonly IUserRepository _userRepository;

	public AuthService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task RegisterAsync(RegisterRequest request)
	{
		var existing = await _userRepository.GetByEmailAsync(request.Email);
		if (existing != null)
			throw new Exception("Email already in use");

		var user = new User
		{
			FullName = request.FullName,
			Email = request.Email,
			//PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
		};

		await _userRepository.CreateAsync(user);
	}
}
