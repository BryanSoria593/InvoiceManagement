namespace InvoiceManagement.Application.Common.Dtos;

public class PagedResultDto<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }

    public PagedResultDto() {}
    public PagedResultDto(List<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }
}
