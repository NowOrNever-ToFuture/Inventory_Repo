using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Common.Interface.RepoInterfaces;

public interface IPurchaseOrderItemRepository : IGenericRepository<PurchaseOrderItem>
{
    Task<List<PurchaseOrderItem>> GetByPurchaseOrderIdsAsync(IEnumerable<Guid> orderIds);
}
