namespace InvoiceManagement.Application.Invoices.Dtos;
public class InvoiceDetailDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
    public string? Description { get; set; }
}
