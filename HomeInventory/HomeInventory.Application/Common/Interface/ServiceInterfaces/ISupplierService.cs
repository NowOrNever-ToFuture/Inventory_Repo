using HomeInventory.Application.Features.Supplier.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface ISupplierService
{
    Task<List<SupplierResponseDto>> GetAllAsync();
    Task<List<SupplierResponseDto>> SuggestAsync(string keyword, int limit = 10);
    Task<SupplierResponseDto?> GetByIdAsync(Guid id);
    Task<SupplierResponseDto> CreateAsync(SupplierRequestDto request);
    Task<SupplierResponseDto> UpdateAsync(Guid id, SupplierRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
