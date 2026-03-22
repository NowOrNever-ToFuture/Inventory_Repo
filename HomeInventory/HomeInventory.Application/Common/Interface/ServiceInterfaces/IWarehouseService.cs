using HomeInventory.Application.Features.Warehouse.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IWarehouseService
{
    Task<List<WarehouseResponseDto>> GetAllAsync();
    Task<WarehouseResponseDto?> GetByIdAsync(Guid id);
    Task<WarehouseResponseDto> CreateAsync(WarehouseRequestDto request);
    Task<WarehouseResponseDto> UpdateAsync(Guid id, WarehouseRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
