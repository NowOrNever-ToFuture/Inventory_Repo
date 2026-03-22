namespace HomeInventory.Application.Common.Exceptions.Entities;

public class CategoryNotFoundException(Guid id)
    : AppException($"Category with id '{id}' was not found.", "CATEGORY_NOT_FOUND", 404)
{
}
