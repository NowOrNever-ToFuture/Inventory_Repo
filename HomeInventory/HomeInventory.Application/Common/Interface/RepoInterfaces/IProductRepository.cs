using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Common.Interface.RepoInterfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> SearchAsync(Guid? brandId, Guid? categoryId, string? normalizedModelKeyword);
    Task<List<Product>> SuggestByModelAsync(string normalizedKeyword, int limit);
    Task<bool> ExistsByModelNormalizedAsync(string normalizedModel, Guid? excludeId = null);
    Task<Product?> GetByModelNormalizedAsync(string normalizedModel);
    Task<Dictionary<Guid, Product>> GetByIdsAsDictionaryAsync(IEnumerable<Guid> ids);
}
