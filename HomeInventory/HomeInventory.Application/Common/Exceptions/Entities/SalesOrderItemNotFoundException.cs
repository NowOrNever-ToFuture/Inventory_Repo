namespace HomeInventory.Application.Common.Exceptions.Entities;

public class SalesOrderItemNotFoundException(Guid id)
    : AppException($"Sales order item with id '{id}' was not found.", "SALES_ORDER_ITEM_NOT_FOUND", 404)
{
}
