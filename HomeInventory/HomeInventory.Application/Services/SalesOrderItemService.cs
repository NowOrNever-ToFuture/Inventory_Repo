using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Exceptions.Entities;
using HomeInventory.Application.Features.SalesOrderItem.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class SalesOrderItemService(IUnitOfWork unitOfWork) : ISalesOrderItemService
{
    public async Task<List<SalesOrderItemResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.SalesOrderItems.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<SalesOrderItemResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.SalesOrderItems.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<SalesOrderItemResponseDto> CreateAsync(SalesOrderItemRequestDto request)
    {
        var entity = new SalesOrderItem
        {
            SalesOrderId = request.SalesOrderId,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        await unitOfWork.SalesOrderItems.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<SalesOrderItemResponseDto> UpdateAsync(Guid id, SalesOrderItemRequestDto request)
    {
        var entity = await unitOfWork.SalesOrderItems.GetByIdAsync(id);
        if (entity is null) throw new SalesOrderItemNotFoundException(id);

        entity.SalesOrderId = request.SalesOrderId;
        entity.ProductId = request.ProductId;
        entity.Quantity = request.Quantity;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.SalesOrderItems.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.SalesOrderItems.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.SalesOrderItems.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static SalesOrderItemResponseDto Map(SalesOrderItem entity) => new()
    {
        Id = entity.Id,
        SalesOrderId = entity.SalesOrderId,
        ProductId = entity.ProductId,
        Quantity = entity.Quantity
    };
}
