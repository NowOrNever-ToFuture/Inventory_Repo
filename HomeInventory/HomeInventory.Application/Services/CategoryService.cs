using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Exceptions.Entities;
using HomeInventory.Application.Common.Extensions;
using HomeInventory.Application.Features.Category.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.Categories.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.Categories.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<CategoryResponseDto> CreateAsync(CategoryRequestDto request)
    {
        var name = request.Name.Trim();
        var normalizedName = request.Name.NormalizeKey();
        if (await unitOfWork.Categories.ExistsByNameNormalizedAsync(normalizedName))
            throw new CategoryAlreadyExistsException(name);

        var entity = new Category
        {
            Name = name,
            Description = request.Description
        };

        await unitOfWork.Categories.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<CategoryResponseDto> UpdateAsync(Guid id, CategoryRequestDto request)
    {
        var entity = await unitOfWork.Categories.GetByIdAsync(id);
        if (entity is null) throw new CategoryNotFoundException(id);

        var name = request.Name.Trim();
        var normalizedName = request.Name.NormalizeKey();
        if (await unitOfWork.Categories.ExistsByNameNormalizedAsync(normalizedName, id))
            throw new CategoryAlreadyExistsException(name);

        entity.Name = name;
        entity.Description = request.Description;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.Categories.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.Categories.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.Categories.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static CategoryResponseDto Map(Category entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description
    };
}
