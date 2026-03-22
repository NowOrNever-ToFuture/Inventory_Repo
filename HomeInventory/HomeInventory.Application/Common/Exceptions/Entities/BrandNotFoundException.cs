namespace HomeInventory.Application.Common.Exceptions.Entities;

public class BrandNotFoundException(Guid id)
    : AppException($"Brand with id '{id}' was not found.", "BRAND_NOT_FOUND", 404)
{
}
