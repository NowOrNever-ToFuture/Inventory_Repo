using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Exceptions.Entities;
using HomeInventory.Application.Common.Extensions;
using HomeInventory.Application.Features.Product.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task<List<ProductResponseDto>> GetAllAsync(Guid? brandId = null, Guid? categoryId = null, string? model = null)
    {
        var keyword = string.IsNullOrWhiteSpace(model) ? null : model.NormalizeKey();
        var entities = await unitOfWork.Products.SearchAsync(brandId, categoryId, keyword);

        return entities.Select(Map).ToList();
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.Products.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<List<ProductResponseDto>> SuggestAsync(string keyword, int limit = 10)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return [];

        var normalized = keyword.NormalizeKey();
        var values = await unitOfWork.Products.SuggestByModelAsync(normalized, limit);

        return values
            .Select(Map)
            .ToList();
    }

    public async Task<ProductResponseDto> CreateAsync(ProductRequestDto request)
    {
        var model = request.Model.Trim();
        var normalizedModel = request.Model.NormalizeKey();
        if (await unitOfWork.Products.ExistsByModelNormalizedAsync(normalizedModel))
            throw new ProductAlreadyExistsException(model);

        var entity = new Product
        {
            Model = model,
            ModelNormalized = normalizedModel,
            Name = request.Name,
            Unit = request.Unit,
            CategoryId = request.CategoryId,
            BrandId = request.BrandId,
            StockQuantity = request.StockQuantity,
            ImportPrice = request.ImportPrice
        };

        await unitOfWork.Products.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<ProductResponseDto> UpdateAsync(Guid id, ProductRequestDto request)
    {
        var entity = await unitOfWork.Products.GetByIdAsync(id);
        if (entity is null) throw new ProductNotFoundException(id);

        var model = request.Model.Trim();
        var normalizedModel = request.Model.NormalizeKey();
        if (await unitOfWork.Products.ExistsByModelNormalizedAsync(normalizedModel, id))
            throw new ProductAlreadyExistsException(model);

        entity.Model = model;
        entity.ModelNormalized = normalizedModel;
        entity.Name = request.Name;
        entity.Unit = request.Unit;
        entity.CategoryId = request.CategoryId;
        entity.BrandId = request.BrandId;
        entity.StockQuantity = request.StockQuantity;
        entity.ImportPrice = request.ImportPrice;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.Products.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
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
        Model = entity.Model,
        Name = entity.Name,
        Unit = entity.Unit,
        CategoryId = entity.CategoryId,
        BrandId = entity.BrandId,
        StockQuantity = entity.StockQuantity,
        ImportPrice = entity.ImportPrice
    };
}
