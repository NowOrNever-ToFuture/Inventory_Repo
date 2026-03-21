using HomeInventory.Domain.Enum;

namespace HomeInventory.Application.Features.Payment.Dtos;

public class PaymentRequestDto
{
    public Guid PurchaseOrderId { get; set; }
    public DateTime PaidAt { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; } = PaymentMethod.BankTransfer;
    public string? Note { get; set; }
}

public class PaymentResponseDto
{
    public Guid Id { get; set; }
    public Guid PurchaseOrderId { get; set; }
    public DateTime PaidAt { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public string? Note { get; set; }
}
