using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Exceptions.Entities;
using HomeInventory.Application.Features.PurchaseOrderItem.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class PurchaseOrderItemService(IUnitOfWork unitOfWork) : IPurchaseOrderItemService
{
    public async Task<List<PurchaseOrderItemResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.PurchaseOrderItems.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<PurchaseOrderItemResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.PurchaseOrderItems.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<PurchaseOrderItemResponseDto> CreateAsync(PurchaseOrderItemRequestDto request)
    {
        var entity = new PurchaseOrderItem
        {
            PurchaseOrderId = request.PurchaseOrderId,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            UnitCost = request.UnitCost,
            LineTotal = request.LineTotal
        };

        await unitOfWork.PurchaseOrderItems.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<PurchaseOrderItemResponseDto> UpdateAsync(Guid id, PurchaseOrderItemRequestDto request)
    {
        var entity = await unitOfWork.PurchaseOrderItems.GetByIdAsync(id);
        if (entity is null) throw new PurchaseOrderItemNotFoundException(id);

        entity.PurchaseOrderId = request.PurchaseOrderId;
        entity.ProductId = request.ProductId;
        entity.Quantity = request.Quantity;
        entity.UnitCost = request.UnitCost;
        entity.LineTotal = request.LineTotal;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.PurchaseOrderItems.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.PurchaseOrderItems.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.PurchaseOrderItems.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static PurchaseOrderItemResponseDto Map(PurchaseOrderItem entity) => new()
    {
        Id = entity.Id,
        PurchaseOrderId = entity.PurchaseOrderId,
        ProductId = entity.ProductId,
        Quantity = entity.Quantity,
        UnitCost = entity.UnitCost,
        LineTotal = entity.LineTotal
    };
}
