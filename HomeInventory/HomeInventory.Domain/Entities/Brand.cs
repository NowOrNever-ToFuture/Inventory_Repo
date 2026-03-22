using HomeInventory.Domain.Common;

namespace HomeInventory.Domain.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
