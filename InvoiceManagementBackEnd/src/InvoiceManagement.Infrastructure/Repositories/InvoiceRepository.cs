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

    public Invoice? GetById(int id)
    {
        return _context.Invoices
            .Include(i => i.InvoiceDetails)
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
