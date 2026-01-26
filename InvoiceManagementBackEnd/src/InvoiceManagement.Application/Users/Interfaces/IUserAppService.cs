using InvoiceManagement.Application.Users;
using InvoiceManagement.Application.Users.Dtos;

namespace InvoiceManagement.Application.Users;
public interface IUserAppService
{
    public List<UserDto> GetAllUsers();
}
