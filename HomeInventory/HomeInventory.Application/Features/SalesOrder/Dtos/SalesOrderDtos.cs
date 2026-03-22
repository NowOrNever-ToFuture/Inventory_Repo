namespace HomeInventory.Application.Features.SalesOrder.Dtos;

public class SalesOrderRequestDto
{
    public string? Code { get; set; }
    public List<SalesOrderCreateItemDto> Items { get; set; } = new();
}

public class SalesOrderCreateItemDto
{
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
}

public class SalesOrderResponseDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public List<SalesOrderResponseItemDto> Items { get; set; } = new();
}

public class SalesOrderResponseItemDto
{
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
}
