using InvoiceManagement.Application.Users.Dtos;
using InvoiceManagement.Domain.Users.Enums;
using InvoiceManagement.Domain.Users.Interfaces;
using InvoiceManagement.Domain.Users.Entities;

namespace InvoiceManagement.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserDto> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username,
                Email = u.Email,
                Status = u.Status
            }).ToList();
        }
    }
}
