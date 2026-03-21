using HomeInventory.Application.Features.Product.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IProductService
{
    Task<List<ProductResponseDto>> GetAllAsync();
    Task<ProductResponseDto?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(ProductRequestDto request);
    Task<bool> UpdateAsync(Guid id, ProductRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
