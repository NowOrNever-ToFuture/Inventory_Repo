using HomeInventory.Application.Features.Warehouse.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IWarehouseService
{
    Task<List<WarehouseResponseDto>> GetAllAsync();
    Task<WarehouseResponseDto?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(WarehouseRequestDto request);
    Task<bool> UpdateAsync(Guid id, WarehouseRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
