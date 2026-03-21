using HomeInventory.Domain.Common;
using HomeInventory.Domain.Enum;

namespace HomeInventory.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid PurchaseOrderId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; } = null!;

    public DateTime PaidAt { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; } = PaymentMethod.BankTransfer;
    public string? Note { get; set; }
}
