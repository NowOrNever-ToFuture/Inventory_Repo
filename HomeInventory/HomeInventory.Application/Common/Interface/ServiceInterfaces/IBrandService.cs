using HomeInventory.Application.Features.Brand.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IBrandService
{
    Task<List<BrandResponseDto>> GetAllAsync();
    Task<List<string>> SuggestAsync(string keyword, int limit = 10);
    Task<BrandResponseDto?> GetByIdAsync(Guid id);
    Task<BrandResponseDto> CreateAsync(BrandRequestDto request);
    Task<BrandResponseDto> UpdateAsync(Guid id, BrandRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
