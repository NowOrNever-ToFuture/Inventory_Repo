using HomeInventory.Application.Features.Category.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface ICategoryService
{
    Task<List<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto?> GetByIdAsync(Guid id);
    Task<CategoryResponseDto> CreateAsync(CategoryRequestDto request);
    Task<CategoryResponseDto> UpdateAsync(Guid id, CategoryRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
