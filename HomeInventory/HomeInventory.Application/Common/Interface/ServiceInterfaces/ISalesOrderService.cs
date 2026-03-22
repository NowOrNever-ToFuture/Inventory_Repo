using HomeInventory.Application.Features.SalesOrder.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface ISalesOrderService
{
    Task<List<SalesOrderResponseDto>> GetAllAsync();
    Task<SalesOrderResponseDto?> GetByIdAsync(Guid id);
    Task<SalesOrderResponseDto> CreateAsync(SalesOrderRequestDto request);
    Task<SalesOrderResponseDto> UpdateAsync(Guid id, SalesOrderRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
