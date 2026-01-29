using InvoiceManagement.Application.Common.Dtos;
using InvoiceManagement.Application.Users;
using InvoiceManagement.Application.Users.Dtos;

namespace InvoiceManagement.Application.Users;
public interface IUserAppService
{
    public List<UserDto> GetAllUsers();
    public UserDto? GetUserById(int id);
    public UserDto RegisterUser(RegisterUserDto dto);
    public UserDto UpdateUser(UpdateUserDto dto);
    public PagedResultDto<UserDto> GetUsers(int pageNumber, int pageSize, string? filter = null);

}
