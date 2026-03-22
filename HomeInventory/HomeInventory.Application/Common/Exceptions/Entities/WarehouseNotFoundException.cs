namespace HomeInventory.Application.Common.Exceptions.Entities;

public class WarehouseNotFoundException(Guid id)
    : AppException($"Warehouse with id '{id}' was not found.", "WAREHOUSE_NOT_FOUND", 404)
{
}
