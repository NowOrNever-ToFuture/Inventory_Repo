using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Domain.Entities;
using HomeInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Infrastructure.Repositories;

public class PurchaseOrderItemRepository(ApplicationDbContext context)
    : GenericRepository<PurchaseOrderItem>(context), IPurchaseOrderItemRepository
{
    public async Task<List<PurchaseOrderItem>> GetByPurchaseOrderIdsAsync(IEnumerable<Guid> orderIds)
    {
        var ids = orderIds.Distinct().ToList();
        if (ids.Count == 0)
            return [];

        return await _dbSet
            .AsNoTracking()
            .Where(x => ids.Contains(x.PurchaseOrderId))
            .ToListAsync();
    }
}
