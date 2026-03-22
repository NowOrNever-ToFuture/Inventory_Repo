using HomeInventory.Domain.Common;

namespace HomeInventory.Domain.Entities;

public class Product : BaseEntity
{
    public string Model { get; set; } = string.Empty;
    public string ModelNormalized { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Unit { get; set; }
    public decimal StockQuantity { get; set; }
    public decimal ImportPrice { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public Guid BrandId { get; set; }
    public Brand Brand { get; set; } = null!;

    public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
    public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
    public ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();
}
