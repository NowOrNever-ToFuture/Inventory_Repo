namespace HomeInventory.Application.Common.Exceptions.Entities;

public class SupplierAlreadyExistsException(string name)
    : AppException($"Supplier '{name}' already exists.", "SUPPLIER_ALREADY_EXISTS", 409)
{
}
