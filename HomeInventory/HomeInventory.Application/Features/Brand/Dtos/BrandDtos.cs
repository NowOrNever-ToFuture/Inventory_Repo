namespace HomeInventory.Application.Features.Brand.Dtos;

public class BrandRequestDto
{
    public string Name { get; set; } = string.Empty;
}

public class BrandResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
