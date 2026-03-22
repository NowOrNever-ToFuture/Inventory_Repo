namespace HomeInventory.Application.Features.Product.Dtos;

public class ProductRequestDto
{
    public string Model { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Unit { get; set; }
    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
    public decimal StockQuantity { get; set; }
    public decimal ImportPrice { get; set; }
}

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Model { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Unit { get; set; }
    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
    public decimal StockQuantity { get; set; }
    public decimal ImportPrice { get; set; }
}
