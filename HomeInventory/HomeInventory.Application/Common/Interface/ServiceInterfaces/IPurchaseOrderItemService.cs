using HomeInventory.Application.Features.PurchaseOrderItem.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IPurchaseOrderItemService
{
    Task<List<PurchaseOrderItemResponseDto>> GetAllAsync();
    Task<PurchaseOrderItemResponseDto?> GetByIdAsync(Guid id);
    Task<PurchaseOrderItemResponseDto> CreateAsync(PurchaseOrderItemRequestDto request);
    Task<PurchaseOrderItemResponseDto> UpdateAsync(Guid id, PurchaseOrderItemRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
