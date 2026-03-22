using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Common.Interface.RepoInterfaces;

public interface IBrandRepository : IGenericRepository<Brand>
{
    Task<bool> ExistsByNameNormalizedAsync(string normalizedName, Guid? excludeId = null);
    Task<List<Brand>> SuggestByNameAsync(string normalizedKeyword, int limit);
}
