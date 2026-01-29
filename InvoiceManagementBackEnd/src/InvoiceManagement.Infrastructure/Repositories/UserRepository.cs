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

    public User? GetById(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted);
    }

    public List<User> GetAll(int pageNumber, int pageSize, string? filter = null)
    {
        var query = _context.Users.Where(u => !u.IsDeleted);
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(u =>
                u.FirstName.Contains(filter) ||
                u.LastName.Contains(filter) ||
                u.Username.Contains(filter) ||
                u.Email.Contains(filter)
            );
        }
        return query
            .OrderBy(u => u.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int GetTotalCount(string? filter = null)
    {
        var query = _context.Users.Where(u => !u.IsDeleted);
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(u =>
                u.FirstName.Contains(filter) ||
                u.LastName.Contains(filter) ||
                u.Username.Contains(filter) ||
                u.Email.Contains(filter)
            );
        }
        return query.Count();
    }

    public List<User> GetAll()
    {
        return _context.Users.Where(u => !u.IsDeleted).ToList();
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

}
