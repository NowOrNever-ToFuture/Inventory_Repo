using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Exceptions.Entities;
using HomeInventory.Application.Common.Extensions;
using HomeInventory.Application.Features.Supplier.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class SupplierService(IUnitOfWork unitOfWork) : ISupplierService
{
    public async Task<List<SupplierResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.Suppliers.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<List<SupplierResponseDto>> SuggestAsync(string keyword, int limit = 10)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return [];

        var normalized = keyword.NormalizeKey();
        var values = await unitOfWork.Suppliers.SuggestByNameAsync(normalized, limit);

        return values
            .Select(Map)
            .ToList();
    }

    public async Task<SupplierResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.Suppliers.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<SupplierResponseDto> CreateAsync(SupplierRequestDto request)
    {
        var name = request.Name.Trim();
        var normalizedName = request.Name.NormalizeKey();
        if (await unitOfWork.Suppliers.ExistsByNameNormalizedAsync(normalizedName))
            throw new SupplierAlreadyExistsException(name);

        var entity = new Supplier
        {
            Name = name,
            Phone = request.Phone,
            Address = request.Address,
            TaxCode = request.TaxCode
        };

        await unitOfWork.Suppliers.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<SupplierResponseDto> UpdateAsync(Guid id, SupplierRequestDto request)
    {
        var entity = await unitOfWork.Suppliers.GetByIdAsync(id);
        if (entity is null) throw new SupplierNotFoundException(id);

        var name = request.Name.Trim();
        var normalizedName = request.Name.NormalizeKey();
        if (await unitOfWork.Suppliers.ExistsByNameNormalizedAsync(normalizedName, id))
            throw new SupplierAlreadyExistsException(name);

        entity.Name = name;
        entity.Phone = request.Phone;
        entity.Address = request.Address;
        entity.TaxCode = request.TaxCode;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.Suppliers.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.Suppliers.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.Suppliers.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static SupplierResponseDto Map(Supplier entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Phone = entity.Phone,
        Address = entity.Address,
        TaxCode = entity.TaxCode
    };
}
