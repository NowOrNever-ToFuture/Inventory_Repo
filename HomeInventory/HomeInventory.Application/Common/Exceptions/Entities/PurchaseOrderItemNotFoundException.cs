namespace HomeInventory.Application.Common.Exceptions.Entities;

public class PurchaseOrderItemNotFoundException(Guid id)
    : AppException($"Purchase order item with id '{id}' was not found.", "PURCHASE_ORDER_ITEM_NOT_FOUND", 404)
{
}
