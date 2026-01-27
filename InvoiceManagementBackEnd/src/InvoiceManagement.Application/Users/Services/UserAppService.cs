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

        public UserDto? GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null || user.IsDeleted)
                return null;
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Status = user.Status
            };
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

        public UserDto RegisterUser(RegisterUserDto dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Email = dto.Email,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                Status = UserStatus.Active
            };
            _userRepository.Add(user);
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Status = user.Status
            };
        }

        public UserDto UpdateUser(UpdateUserDto dto)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == dto.Id);
            if (user == null)
                throw new Exception("Usuario no encontrado");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Username = dto.Username;
            user.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }
            _userRepository.Update(user);
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Status = user.Status
            };
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }
    }
}
