using HomeInventory.Application.Features.PurchaseOrder.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IPurchaseOrderService
{
    Task<List<PurchaseOrderResponseDto>> GetAllAsync();
    Task<PurchaseOrderResponseDto?> GetByIdAsync(Guid id);
    Task<PurchaseOrderResponseDto> CreateAsync(PurchaseOrderRequestDto request);
    Task<PurchaseOrderResponseDto> UpdateAsync(Guid id, PurchaseOrderRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
