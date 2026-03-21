using HomeInventory.Domain.Common;
using HomeInventory.Domain.Enum;

namespace HomeInventory.Domain.Entities;

public class PurchaseOrder : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Draft;

    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public decimal SubTotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }

    public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
