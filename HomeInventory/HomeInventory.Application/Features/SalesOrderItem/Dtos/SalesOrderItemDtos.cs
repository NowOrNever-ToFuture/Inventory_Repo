namespace HomeInventory.Application.Features.SalesOrderItem.Dtos;

public class SalesOrderItemRequestDto
{
    public Guid SalesOrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
}

public class SalesOrderItemResponseDto
{
    public Guid Id { get; set; }
    public Guid SalesOrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
}
