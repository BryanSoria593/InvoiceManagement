using System.Collections.Generic;
using InvoiceManagement.Domain.Invoices.Entities;
using InvoiceManagement.Domain.Invoices.Enums;
using InvoiceManagement.Domain.Invoices.Interfaces;
using InvoiceManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly InvoiceManagementDbContext _context;

    public InvoiceRepository(InvoiceManagementDbContext context)
    {
        _context = context;
    }

    public List<Invoice> GetAll()
    {
        return _context.Invoices
            .Where(i => !i.IsDeleted)
            .Include(i => i.InvoiceDetails)
            .ToList();
    }

    public List<Invoice> GetAll(int pageNumber, int pageSize, string? filter = null)
    {
        IQueryable<Invoice> query = _context.Invoices
            .Where(i => !i.IsDeleted)
            .Include(i => i.Customer)
            .Include(i => i.PaymentMethod)
            .Include(i => i.User);

        if (!string.IsNullOrEmpty(filter))
        {
            filter = filter.Trim();
            int invoiceId;
            bool isNumeric = int.TryParse(filter, out invoiceId);
            query = query.Where(i =>
                (isNumeric && i.Id == invoiceId) ||
                (i.Customer != null && i.Customer.Name.Contains(filter))
            );
        }
        return query
            .OrderByDescending(i => i.Date)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int GetTotalCount(string? filter = null)
    {
        IQueryable<Invoice> query = _context.Invoices
            .Where(i => !i.IsDeleted)
            .Include(i => i.Customer);

        if (!string.IsNullOrEmpty(filter))
        {
            filter = filter.Trim();
            int invoiceId;
            bool isNumeric = int.TryParse(filter, out invoiceId);
            query = query.Where(i =>
                (isNumeric && i.Id == invoiceId) ||
                (i.Customer != null && i.Customer.Name.Contains(filter))
            );
        }
        return query.Count();
    }

    public Invoice? GetById(int id)
    {
        return _context.Invoices
            .Include(i => i.Customer)
            .Include(i => i.InvoiceDetails)
                .ThenInclude(d => d.Product).Where(i => !i.IsDeleted)
            .FirstOrDefault(i => i.Id == id && !i.IsDeleted);
    }


    public void Add(Invoice invoice)
    {
        _context.Invoices.Add(invoice);
        _context.SaveChanges();
    }

    public void Update(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        _context.SaveChanges();
    }

}
