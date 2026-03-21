using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
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

    public async Task<Guid> CreateAsync(CategoryRequestDto request)
    {
        var entity = new Category
        {
            Name = request.Name,
            Description = request.Description
        };

        await unitOfWork.Categories.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, CategoryRequestDto request)
    {
        var entity = await unitOfWork.Categories.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.Categories.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
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
