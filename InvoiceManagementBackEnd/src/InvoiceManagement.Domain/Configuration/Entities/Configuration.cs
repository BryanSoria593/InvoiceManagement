using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagement.Domain.Configuration.Entities;

public class Configuration
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal VatPercentage { get; set; }
    public string CurrencySymbol { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
}
