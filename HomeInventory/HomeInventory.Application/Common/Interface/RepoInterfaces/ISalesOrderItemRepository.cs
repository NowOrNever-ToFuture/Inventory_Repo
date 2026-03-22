using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Common.Interface.RepoInterfaces;

public interface ISalesOrderItemRepository : IGenericRepository<SalesOrderItem>
{
    Task<List<SalesOrderItem>> GetBySalesOrderIdsAsync(IEnumerable<Guid> orderIds);
}
