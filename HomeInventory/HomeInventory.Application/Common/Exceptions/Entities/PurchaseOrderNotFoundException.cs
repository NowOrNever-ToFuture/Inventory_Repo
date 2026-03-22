namespace HomeInventory.Application.Common.Exceptions.Entities;

public class PurchaseOrderNotFoundException(Guid id)
    : AppException($"Purchase order with id '{id}' was not found.", "PURCHASE_ORDER_NOT_FOUND", 404)
{
}
