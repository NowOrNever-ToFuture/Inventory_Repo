using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Exceptions.Entities;
using HomeInventory.Application.Common.Extensions;
using HomeInventory.Application.Features.Brand.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class BrandService(IUnitOfWork unitOfWork) : IBrandService
{
    public async Task<List<BrandResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.Brands.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<List<string>> SuggestAsync(string keyword, int limit = 10)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return [];

        var normalized = keyword.NormalizeKey();
        var values = await unitOfWork.Brands.SuggestByNameAsync(normalized, limit);

        return values
            .Select(x => x.Name)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public async Task<BrandResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.Brands.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<BrandResponseDto> CreateAsync(BrandRequestDto request)
    {
        var name = request.Name.Trim();
        var normalizedName = request.Name.NormalizeKey();
        if (await unitOfWork.Brands.ExistsByNameNormalizedAsync(normalizedName))
            throw new BrandAlreadyExistsException(name);

        var entity = new Brand
        {
            Name = name
        };

        await unitOfWork.Brands.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<BrandResponseDto> UpdateAsync(Guid id, BrandRequestDto request)
    {
        var entity = await unitOfWork.Brands.GetByIdAsync(id);
        if (entity is null) throw new BrandNotFoundException(id);

        var name = request.Name.Trim();
        var normalizedName = request.Name.NormalizeKey();
        if (await unitOfWork.Brands.ExistsByNameNormalizedAsync(normalizedName, id))
            throw new BrandAlreadyExistsException(name);

        entity.Name = name;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.Brands.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.Brands.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.Brands.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static BrandResponseDto Map(Brand entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name
    };
}
