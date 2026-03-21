using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Features.InventoryTransaction.Dtos;
using HomeInventory.Domain.Entities;

namespace HomeInventory.Application.Services;

public class InventoryTransactionService(IUnitOfWork unitOfWork) : IInventoryTransactionService
{
    public async Task<List<InventoryTransactionResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.InventoryTransactions.GetAllAsync();
        return entities.Select(Map).ToList();
    }

    public async Task<InventoryTransactionResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.InventoryTransactions.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<Guid> CreateAsync(InventoryTransactionRequestDto request)
    {
        var entity = new InventoryTransaction
        {
            ProductId = request.ProductId,
            WarehouseId = request.WarehouseId,
            Type = request.Type,
            Quantity = request.Quantity,
            UnitCost = request.UnitCost,
            TransactionDate = request.TransactionDate,
            ReferenceType = request.ReferenceType,
            ReferenceId = request.ReferenceId,
            Note = request.Note
        };

        await unitOfWork.InventoryTransactions.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, InventoryTransactionRequestDto request)
    {
        var entity = await unitOfWork.InventoryTransactions.GetByIdAsync(id);
        if (entity is null) return false;

        entity.ProductId = request.ProductId;
        entity.WarehouseId = request.WarehouseId;
        entity.Type = request.Type;
        entity.Quantity = request.Quantity;
        entity.UnitCost = request.UnitCost;
        entity.TransactionDate = request.TransactionDate;
        entity.ReferenceType = request.ReferenceType;
        entity.ReferenceId = request.ReferenceId;
        entity.Note = request.Note;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.InventoryTransactions.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.InventoryTransactions.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.InventoryTransactions.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static InventoryTransactionResponseDto Map(InventoryTransaction entity) => new()
    {
        Id = entity.Id,
        ProductId = entity.ProductId,
        WarehouseId = entity.WarehouseId,
        Type = entity.Type,
        Quantity = entity.Quantity,
        UnitCost = entity.UnitCost,
        TransactionDate = entity.TransactionDate,
        ReferenceType = entity.ReferenceType,
        ReferenceId = entity.ReferenceId,
        Note = entity.Note
    };
}
