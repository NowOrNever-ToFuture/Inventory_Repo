namespace HomeInventory.Application.Features.PurchaseOrder.Dtos;

public class PurchaseOrderRequestDto
{
    public string? Code { get; set; }
    public Guid SupplierId { get; set; }
    public List<PurchaseOrderCreateItemDto> Items { get; set; } = new();
}

public class PurchaseOrderCreateItemDto
{
    public string Model { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Unit { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
}

public class PurchaseOrderResponseDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public Guid SupplierId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<PurchaseOrderResponseItemDto> Items { get; set; } = new();
}

public class PurchaseOrderResponseItemDto
{
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal LineTotal { get; set; }
}
