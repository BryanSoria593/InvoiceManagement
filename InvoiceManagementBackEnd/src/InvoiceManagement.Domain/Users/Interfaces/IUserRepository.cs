
using InvoiceManagement.Domain.Users.Entities;

namespace InvoiceManagement.Domain.Users.Interfaces;
public interface IUserRepository
{
    List<User> GetAll();
    User? GetById(int id);
    void Add(User user);
    void Update(User user);

    List<User> GetAll(int pageNumber, int pageSize, string? filter = null);
    int GetTotalCount(string? filter = null);
}
