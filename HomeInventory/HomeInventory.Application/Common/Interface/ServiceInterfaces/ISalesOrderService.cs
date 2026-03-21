using HomeInventory.Application.Features.SalesOrder.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface ISalesOrderService
{
    Task<List<SalesOrderResponseDto>> GetAllAsync();
    Task<SalesOrderResponseDto?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(SalesOrderRequestDto request);
    Task<bool> UpdateAsync(Guid id, SalesOrderRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
