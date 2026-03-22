namespace HomeInventory.Application.Common.Exceptions.Entities;

public class CategoryAlreadyExistsException(string name)
    : AppException($"Category '{name}' already exists.", "CATEGORY_ALREADY_EXISTS", 409)
{
}
