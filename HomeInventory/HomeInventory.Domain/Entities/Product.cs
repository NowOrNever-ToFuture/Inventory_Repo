using HomeInventory.Domain.Common;

namespace HomeInventory.Domain.Entities;

public class Product : BaseEntity
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Unit { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public decimal DefaultCostPrice { get; set; }
    public decimal DefaultSellPrice { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
    public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
    public ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();
}
