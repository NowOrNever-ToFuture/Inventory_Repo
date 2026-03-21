using HomeInventory.Application.Features.InventoryTransaction.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IInventoryTransactionService
{
    Task<List<InventoryTransactionResponseDto>> GetAllAsync();
    Task<InventoryTransactionResponseDto?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(InventoryTransactionRequestDto request);
    Task<bool> UpdateAsync(Guid id, InventoryTransactionRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
