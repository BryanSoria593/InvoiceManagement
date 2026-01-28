using InvoiceManagement.Application.Auth.Dtos;
using InvoiceManagement.Application.Auth.Interfaces;
using InvoiceManagement.Domain.Users.Interfaces;

namespace InvoiceManagement.Application.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public LoginResponseDto? Login(LoginRequestDto dto)
    {
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == dto.Email && !u.IsDeleted);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return null;

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Email);
        return new LoginResponseDto
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddHours(2)
        };
    }
}
