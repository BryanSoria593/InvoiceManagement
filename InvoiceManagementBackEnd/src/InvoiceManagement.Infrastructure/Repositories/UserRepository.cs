using InvoiceManagement.Domain.Users.Entities;
using InvoiceManagement.Domain.Users.Interfaces;
using InvoiceManagement.Domain.Users.Enums;

namespace InvoiceManagement.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    public List<User> GetAll()
    {
        return new List<User>
        {
            new User { Id = 1, FirstName = "John", LastName = "Doe", Username = "johndoe", Email = "john@example.com", Status = UserStatus.Active, CreatedAt = DateTime.UtcNow },
            new User { Id = 2, FirstName = "Jane", LastName = "Smith", Username = "janesmith", Email = "jane@example.com", Status = UserStatus.Inactive, CreatedAt = DateTime.UtcNow }
        };
    }
}
