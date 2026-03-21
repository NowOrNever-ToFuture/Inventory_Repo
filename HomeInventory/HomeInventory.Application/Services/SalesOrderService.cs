using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Features.SalesOrder.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class SalesOrderService(IUnitOfWork unitOfWork) : ISalesOrderService
{
    public async Task<List<SalesOrderResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.SalesOrders.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<SalesOrderResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.SalesOrders.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<Guid> CreateAsync(SalesOrderRequestDto request)
    {
        var entity = new SalesOrder
        {
            Code = request.Code,
            OrderDate = request.OrderDate,
            Status = request.Status,
            SubTotalAmount = request.SubTotalAmount,
            DiscountAmount = request.DiscountAmount,
            TotalAmount = request.TotalAmount
        };

        await unitOfWork.SalesOrders.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, SalesOrderRequestDto request)
    {
        var entity = await unitOfWork.SalesOrders.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Code = request.Code;
        entity.OrderDate = request.OrderDate;
        entity.Status = request.Status;
        entity.SubTotalAmount = request.SubTotalAmount;
        entity.DiscountAmount = request.DiscountAmount;
        entity.TotalAmount = request.TotalAmount;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.SalesOrders.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.SalesOrders.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.SalesOrders.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static SalesOrderResponseDto Map(SalesOrder entity) => new()
    {
        Id = entity.Id,
        Code = entity.Code,
        OrderDate = entity.OrderDate,
        Status = entity.Status,
        SubTotalAmount = entity.SubTotalAmount,
        DiscountAmount = entity.DiscountAmount,
        TotalAmount = entity.TotalAmount
    };
}
