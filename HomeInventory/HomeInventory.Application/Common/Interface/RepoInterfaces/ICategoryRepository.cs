using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Common.Interface.RepoInterfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> ExistsByNameNormalizedAsync(string normalizedName, Guid? excludeId = null);
}
