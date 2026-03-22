using HomeInventory.Application.Features.Product.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IProductService
{
    Task<List<ProductResponseDto>> GetAllAsync(Guid? brandId = null, Guid? categoryId = null, string? model = null);
    Task<List<ProductResponseDto>> SuggestAsync(string keyword, int limit = 10);
    Task<ProductResponseDto?> GetByIdAsync(Guid id);
    Task<ProductResponseDto> CreateAsync(ProductRequestDto request);
    Task<ProductResponseDto> UpdateAsync(Guid id, ProductRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
