using HomeInventory.Application.Features.Supplier.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface ISupplierService
{
    Task<List<SupplierResponseDto>> GetAllAsync();
    Task<SupplierResponseDto?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(SupplierRequestDto request);
    Task<bool> UpdateAsync(Guid id, SupplierRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
