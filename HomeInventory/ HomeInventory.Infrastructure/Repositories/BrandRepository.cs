using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Domain.Entities;
using HomeInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Infrastructure.Repositories;

public class BrandRepository(ApplicationDbContext context)
    : GenericRepository<Brand>(context), IBrandRepository
{
    public async Task<bool> ExistsByNameNormalizedAsync(string normalizedName, Guid? excludeId = null)
    {
        var query = _dbSet.AsNoTracking().Where(x => x.Name.ToLower() == normalizedName.ToLower());

        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return await query.AnyAsync();
    }

    public async Task<List<Brand>> SuggestByNameAsync(string normalizedKeyword, int limit)
    {
        var loweredKeyword = normalizedKeyword.ToLower();

        return await _dbSet
            .AsNoTracking()
            .Where(x => x.Name.ToLower().StartsWith(loweredKeyword) || x.Name.ToLower().Contains(loweredKeyword))
            .OrderBy(x => x.Name)
            .Take(limit)
            .ToListAsync();
    }
}
