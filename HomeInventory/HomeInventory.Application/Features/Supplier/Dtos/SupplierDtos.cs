namespace HomeInventory.Application.Features.Supplier.Dtos;

public class SupplierRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxCode { get; set; }
}

public class SupplierResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxCode { get; set; }
}
