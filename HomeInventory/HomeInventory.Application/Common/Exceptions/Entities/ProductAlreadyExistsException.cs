namespace HomeInventory.Application.Common.Exceptions.Entities;

public class ProductAlreadyExistsException(string model)
    : AppException($"Product model '{model}' already exists.", "PRODUCT_ALREADY_EXISTS", 409)
{
}
