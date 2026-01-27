
using InvoiceManagement.Domain.Users.Entities;

namespace InvoiceManagement.Domain.Users.Interfaces;
public interface IUserRepository
{
    List<User> GetAll();
    void Add(User user);
}
