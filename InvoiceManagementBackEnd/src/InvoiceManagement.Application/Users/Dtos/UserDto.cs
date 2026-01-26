using InvoiceManagement.Domain.Users;
using InvoiceManagement.Domain.Users.Enums;

namespace InvoiceManagement.Application.Users.Dtos;
public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserStatus Status { get; set; }
}
