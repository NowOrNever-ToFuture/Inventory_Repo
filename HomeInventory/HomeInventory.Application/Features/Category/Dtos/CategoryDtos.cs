namespace HomeInventory.Application.Features.Category.Dtos;

public class CategoryRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
