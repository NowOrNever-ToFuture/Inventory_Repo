namespace HomeInventory.Application.Common.Exceptions.Entities;

public class SupplierNotFoundException(Guid id)
    : AppException($"Supplier with id '{id}' was not found.", "SUPPLIER_NOT_FOUND", 404)
{
}
