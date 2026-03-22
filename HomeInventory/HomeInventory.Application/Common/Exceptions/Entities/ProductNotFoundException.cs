namespace HomeInventory.Application.Common.Exceptions.Entities;

public class ProductNotFoundException(Guid id)
    : AppException($"Product with id '{id}' was not found.", "PRODUCT_NOT_FOUND", 404)
{
}
