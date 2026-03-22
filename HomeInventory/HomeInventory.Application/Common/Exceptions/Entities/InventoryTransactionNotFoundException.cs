namespace HomeInventory.Application.Common.Exceptions.Entities;

public class InventoryTransactionNotFoundException(Guid id)
    : AppException($"Inventory transaction with id '{id}' was not found.", "INVENTORY_TRANSACTION_NOT_FOUND", 404)
{
}
