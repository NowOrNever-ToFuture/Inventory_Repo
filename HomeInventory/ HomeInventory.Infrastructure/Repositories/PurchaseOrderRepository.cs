using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Domain.Entities;
using HomeInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Infrastructure.Repositories;

public class PurchaseOrderRepository(ApplicationDbContext context)
    : GenericRepository<PurchaseOrder>(context), IPurchaseOrderRepository
{
    public async Task<List<PurchaseOrder>> GetByYearAndMonthAsync(int year, int? month = null)
    {
        var query = _dbSet.AsNoTracking().Where(x => x.OrderDate.Year == year);

        if (month.HasValue)
            query = query.Where(x => x.OrderDate.Month == month.Value);

        return await query.ToListAsync();
    }

    public async Task<List<(int Year, int Month, decimal TotalAmount)>> GetImportSummaryAsync(int year, int? month = null)
    {
        var query = _dbSet.AsNoTracking().Where(x => x.OrderDate.Year == year);

        if (month.HasValue)
            query = query.Where(x => x.OrderDate.Month == month.Value);

        return await query
            .GroupBy(x => new { x.OrderDate.Year, x.OrderDate.Month })
            .Select(g => new ValueTuple<int, int, decimal>(g.Key.Year, g.Key.Month, g.Sum(x => x.TotalAmount)))
            .OrderBy(x => x.Item2)
            .ToListAsync();
    }
}
