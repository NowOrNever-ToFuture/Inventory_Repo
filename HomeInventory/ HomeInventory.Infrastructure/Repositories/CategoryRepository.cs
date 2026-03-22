using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Domain.Entities;
using HomeInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Infrastructure.Repositories;

public class CategoryRepository(ApplicationDbContext context)
    : GenericRepository<Category>(context), ICategoryRepository
{
    public async Task<bool> ExistsByNameNormalizedAsync(string normalizedName, Guid? excludeId = null)
    {
        var query = _dbSet.AsNoTracking().Where(x => x.Name.ToLower() == normalizedName.ToLower());

        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return await query.AnyAsync();
    }
}
