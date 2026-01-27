
using InvoiceManagement.Domain.Users.Entities;

namespace InvoiceManagement.Domain.Users.Interfaces;
public interface IUserRepository
{
    List<User> GetAll();
    User? GetById(int id);
    void Add(User user);
    void Update(User user);
    void Delete(int id);
}
