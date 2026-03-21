namespace HomeInventory.Application.Features.Product.Dtos;

public class ProductRequestDto
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Unit { get; set; }
    public Guid CategoryId { get; set; }
    public decimal DefaultCostPrice { get; set; }
    public decimal DefaultSellPrice { get; set; }
    public bool IsActive { get; set; } = true;
}

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Unit { get; set; }
    public Guid CategoryId { get; set; }
    public decimal DefaultCostPrice { get; set; }
    public decimal DefaultSellPrice { get; set; }
    public bool IsActive { get; set; }
}
