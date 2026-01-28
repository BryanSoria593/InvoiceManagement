using InvoiceManagement.Application.Auth.Dtos;

namespace InvoiceManagement.Application.Auth.Interfaces;

public interface IAuthService
{
    LoginResponseDto? Login(LoginRequestDto dto);
}
