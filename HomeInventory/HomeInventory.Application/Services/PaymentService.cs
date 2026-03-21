using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Features.Payment.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class PaymentService(IUnitOfWork unitOfWork) : IPaymentService
{
    public async Task<List<PaymentResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.Payments.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<PaymentResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.Payments.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<Guid> CreateAsync(PaymentRequestDto request)
    {
        var entity = new Payment
        {
            PurchaseOrderId = request.PurchaseOrderId,
            PaidAt = request.PaidAt,
            Amount = request.Amount,
            Method = request.Method,
            Note = request.Note
        };

        await unitOfWork.Payments.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, PaymentRequestDto request)
    {
        var entity = await unitOfWork.Payments.GetByIdAsync(id);
        if (entity is null) return false;

        entity.PurchaseOrderId = request.PurchaseOrderId;
        entity.PaidAt = request.PaidAt;
        entity.Amount = request.Amount;
        entity.Method = request.Method;
        entity.Note = request.Note;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.Payments.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.Payments.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.Payments.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static PaymentResponseDto Map(Payment entity) => new()
    {
        Id = entity.Id,
        PurchaseOrderId = entity.PurchaseOrderId,
        PaidAt = entity.PaidAt,
        Amount = entity.Amount,
        Method = entity.Method,
        Note = entity.Note
    };
}
