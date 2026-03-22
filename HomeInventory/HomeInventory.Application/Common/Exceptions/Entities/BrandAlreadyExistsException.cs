namespace HomeInventory.Application.Common.Exceptions.Entities;

public class BrandAlreadyExistsException(string name)
    : AppException($"Brand '{name}' already exists.", "BRAND_ALREADY_EXISTS", 409)
{
}
