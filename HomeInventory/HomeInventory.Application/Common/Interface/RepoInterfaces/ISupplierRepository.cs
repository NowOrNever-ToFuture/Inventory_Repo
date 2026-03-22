using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Common.Interface.RepoInterfaces;

public interface ISupplierRepository : IGenericRepository<Supplier>
{
    Task<bool> ExistsByNameNormalizedAsync(string normalizedName, Guid? excludeId = null);
    Task<List<Supplier>> SuggestByNameAsync(string normalizedKeyword, int limit);
}
