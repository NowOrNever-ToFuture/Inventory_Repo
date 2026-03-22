using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Common.Interface.RepoInterfaces;

public interface IPurchaseOrderRepository : IGenericRepository<PurchaseOrder>
{
    Task<List<PurchaseOrder>> GetByYearAndMonthAsync(int year, int? month = null);
    Task<List<(int Year, int Month, decimal TotalAmount)>> GetImportSummaryAsync(int year, int? month = null);
}
