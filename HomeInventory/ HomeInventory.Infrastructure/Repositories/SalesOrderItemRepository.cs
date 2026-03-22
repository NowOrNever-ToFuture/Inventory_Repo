using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Domain.Entities;
using HomeInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Infrastructure.Repositories;

public class SalesOrderItemRepository(ApplicationDbContext context)
    : GenericRepository<SalesOrderItem>(context), ISalesOrderItemRepository
{
    public async Task<List<SalesOrderItem>> GetBySalesOrderIdsAsync(IEnumerable<Guid> orderIds)
    {
        var ids = orderIds.Distinct().ToList();
        if (ids.Count == 0)
            return [];

        return await _dbSet
            .AsNoTracking()
            .Where(x => ids.Contains(x.SalesOrderId))
            .ToListAsync();
    }
}
