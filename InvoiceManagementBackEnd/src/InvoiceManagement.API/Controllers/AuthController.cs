using InvoiceManagement.Application.Auth.Dtos;
using InvoiceManagement.Application.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto dto)
    {
        var result = _authService.Login(dto);
        if (result == null)
            return Unauthorized("Usuario o contraseña incorrectos");
        return Ok(result);
    }
}
