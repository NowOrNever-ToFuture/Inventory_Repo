using HomeInventory.Application.Features.InventoryTransaction.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IInventoryTransactionService
{
    Task<List<InventoryTransactionResponseDto>> GetAllAsync();
    Task<InventoryTransactionResponseDto?> GetByIdAsync(Guid id);
    Task<InventoryTransactionResponseDto> CreateAsync(InventoryTransactionRequestDto request);
    Task<InventoryTransactionResponseDto> UpdateAsync(Guid id, InventoryTransactionRequestDto request);
    Task<bool> DeleteAsync(Guid id);
}
