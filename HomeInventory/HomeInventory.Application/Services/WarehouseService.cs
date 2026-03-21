using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Features.Warehouse.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class WarehouseService(IUnitOfWork unitOfWork) : IWarehouseService
{
    public async Task<List<WarehouseResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.Warehouses.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<WarehouseResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.Warehouses.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<Guid> CreateAsync(WarehouseRequestDto request)
    {
        var entity = new Warehouse
        {
            Code = request.Code,
            Name = request.Name,
            Address = request.Address
        };

        await unitOfWork.Warehouses.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, WarehouseRequestDto request)
    {
        var entity = await unitOfWork.Warehouses.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Code = request.Code;
        entity.Name = request.Name;
        entity.Address = request.Address;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.Warehouses.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.Warehouses.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.Warehouses.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static WarehouseResponseDto Map(Warehouse entity) => new()
    {
        Id = entity.Id,
        Code = entity.Code,
        Name = entity.Name,
        Address = entity.Address
    };
}
