using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Features.Product.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.Products.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.Products.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<Guid> CreateAsync(ProductRequestDto request)
    {
        var entity = new Product
        {
            Sku = request.Sku,
            Name = request.Name,
            Unit = request.Unit,
            CategoryId = request.CategoryId,
            DefaultCostPrice = request.DefaultCostPrice,
            DefaultSellPrice = request.DefaultSellPrice,
            IsActive = request.IsActive
        };

        await unitOfWork.Products.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, ProductRequestDto request)
    {
        var entity = await unitOfWork.Products.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Sku = request.Sku;
        entity.Name = request.Name;
        entity.Unit = request.Unit;
        entity.CategoryId = request.CategoryId;
        entity.DefaultCostPrice = request.DefaultCostPrice;
        entity.DefaultSellPrice = request.DefaultSellPrice;
        entity.IsActive = request.IsActive;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.Products.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.Products.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.Products.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static ProductResponseDto Map(Product entity) => new()
    {
        Id = entity.Id,
        Sku = entity.Sku,
        Name = entity.Name,
        Unit = entity.Unit,
        CategoryId = entity.CategoryId,
        DefaultCostPrice = entity.DefaultCostPrice,
        DefaultSellPrice = entity.DefaultSellPrice,
        IsActive = entity.IsActive
    };
}
