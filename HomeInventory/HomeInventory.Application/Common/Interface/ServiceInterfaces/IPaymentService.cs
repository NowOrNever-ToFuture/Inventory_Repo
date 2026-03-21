using HomeInventory.Application.Features.Payment.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IPaymentService
{
    Task<List<PaymentResponseDto>> GetAllAsync();
    Task<PaymentResponseDto?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(PaymentRequestDto request);
    Task<bool> UpdateAsync(Guid id, PaymentRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
