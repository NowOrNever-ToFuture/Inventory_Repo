namespace HomeInventory.Application.Common.Interface.RepoInterfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository Categories { get; }
    ISupplierRepository Suppliers { get; }
    IWarehouseRepository Warehouses { get; }
    IProductRepository Products { get; }
    IPurchaseOrderRepository PurchaseOrders { get; }
    IPurchaseOrderItemRepository PurchaseOrderItems { get; }
    ISalesOrderRepository SalesOrders { get; }
    ISalesOrderItemRepository SalesOrderItems { get; }
    IPaymentRepository Payments { get; }
    IInventoryTransactionRepository InventoryTransactions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
