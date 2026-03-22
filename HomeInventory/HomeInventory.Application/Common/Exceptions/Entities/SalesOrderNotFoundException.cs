namespace HomeInventory.Application.Common.Exceptions.Entities;

public class SalesOrderNotFoundException(Guid id)
    : AppException($"Sales order with id '{id}' was not found.", "SALES_ORDER_NOT_FOUND", 404)
{
}
