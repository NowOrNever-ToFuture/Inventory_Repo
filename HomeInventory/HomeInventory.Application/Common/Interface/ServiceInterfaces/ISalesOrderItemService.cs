using HomeInventory.Application.Features.SalesOrderItem.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface ISalesOrderItemService
{
    Task<List<SalesOrderItemResponseDto>> GetAllAsync();
    Task<SalesOrderItemResponseDto?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(SalesOrderItemRequestDto request);
    Task<bool> UpdateAsync(Guid id, SalesOrderItemRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
