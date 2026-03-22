using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Domain.Entities;
using HomeInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext context)
    : GenericRepository<Product>(context), IProductRepository
{
    public async Task<List<Product>> SearchAsync(Guid? brandId, Guid? categoryId, string? normalizedModelKeyword)
    {
        var query = _dbSet.AsNoTracking().AsQueryable();

        if (brandId.HasValue)
            query = query.Where(x => x.BrandId == brandId.Value);

        if (categoryId.HasValue)
            query = query.Where(x => x.CategoryId == categoryId.Value);

        if (!string.IsNullOrWhiteSpace(normalizedModelKeyword))
        {
            var loweredKeyword = normalizedModelKeyword.ToLower();
            query = query.Where(x => x.ModelNormalized.ToLower().Contains(loweredKeyword));
        }

        return await query.ToListAsync();
    }

    public async Task<List<Product>> SuggestByModelAsync(string normalizedKeyword, int limit)
    {
        var loweredKeyword = normalizedKeyword.ToLower();

        return await _dbSet
            .AsNoTracking()
            .Where(x => x.Model.ToLower().StartsWith(loweredKeyword) || x.Model.ToLower().Contains(loweredKeyword))
            .OrderBy(x => x.Model)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<bool> ExistsByModelNormalizedAsync(string normalizedModel, Guid? excludeId = null)
    {
        var query = _dbSet.AsNoTracking().Where(x => x.ModelNormalized.ToLower() == normalizedModel.ToLower());

        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return await query.AnyAsync();
    }

    public async Task<Product?> GetByModelNormalizedAsync(string normalizedModel)
    {
        var loweredModel = normalizedModel.ToLower();

        return await _dbSet
            .FirstOrDefaultAsync(x => x.ModelNormalized.ToLower() == loweredModel);
    }

    public async Task<Dictionary<Guid, Product>> GetByIdsAsDictionaryAsync(IEnumerable<Guid> ids)
    {
        var idList = ids.Distinct().ToList();

        if (idList.Count == 0)
            return new Dictionary<Guid, Product>();

        return await _dbSet
            .Where(x => idList.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id);
    }
}
