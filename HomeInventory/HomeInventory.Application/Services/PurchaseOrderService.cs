using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Features.PurchaseOrder.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class PurchaseOrderService(IUnitOfWork unitOfWork) : IPurchaseOrderService
{
    public async Task<List<PurchaseOrderResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.PurchaseOrders.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<PurchaseOrderResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.PurchaseOrders.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<Guid> CreateAsync(PurchaseOrderRequestDto request)
    {
        var entity = new PurchaseOrder
        {
            Code = request.Code,
            OrderDate = request.OrderDate,
            Status = request.Status,
            SupplierId = request.SupplierId,
            SubTotalAmount = request.SubTotalAmount,
            DiscountAmount = request.DiscountAmount,
            TotalAmount = request.TotalAmount
        };

        await unitOfWork.PurchaseOrders.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, PurchaseOrderRequestDto request)
    {
        var entity = await unitOfWork.PurchaseOrders.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Code = request.Code;
        entity.OrderDate = request.OrderDate;
        entity.Status = request.Status;
        entity.SupplierId = request.SupplierId;
        entity.SubTotalAmount = request.SubTotalAmount;
        entity.DiscountAmount = request.DiscountAmount;
        entity.TotalAmount = request.TotalAmount;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.PurchaseOrders.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.PurchaseOrders.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.PurchaseOrders.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static PurchaseOrderResponseDto Map(PurchaseOrder entity) => new()
    {
        Id = entity.Id,
        Code = entity.Code,
        OrderDate = entity.OrderDate,
        Status = entity.Status,
        SupplierId = entity.SupplierId,
        SubTotalAmount = entity.SubTotalAmount,
        DiscountAmount = entity.DiscountAmount,
        TotalAmount = entity.TotalAmount
    };
}
