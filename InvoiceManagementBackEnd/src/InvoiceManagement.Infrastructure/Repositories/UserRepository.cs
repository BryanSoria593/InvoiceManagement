using InvoiceManagement.Domain.Users.Entities;
using InvoiceManagement.Domain.Users.Interfaces;
using InvoiceManagement.Domain.Users.Enums;
using InvoiceManagement.Infrastructure.Persistence;

namespace InvoiceManagement.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly InvoiceManagementDbContext _context;

    public UserRepository(InvoiceManagementDbContext context)
    {
        _context = context;
    }

    public List<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}
