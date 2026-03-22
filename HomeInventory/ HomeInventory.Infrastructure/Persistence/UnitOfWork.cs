using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Infrastructure.Data;
using HomeInventory.Infrastructure.Repositories;

namespace HomeInventory.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Categories = new CategoryRepository(_context);
        Brands = new BrandRepository(_context);
        Suppliers = new SupplierRepository(_context);
        Warehouses = new WarehouseRepository(_context);
        Products = new ProductRepository(_context);
        PurchaseOrders = new PurchaseOrderRepository(_context);
        PurchaseOrderItems = new PurchaseOrderItemRepository(_context);
        SalesOrders = new SalesOrderRepository(_context);
        SalesOrderItems = new SalesOrderItemRepository(_context);
        InventoryTransactions = new InventoryTransactionRepository(_context);
    }

    public ICategoryRepository Categories { get; }
    public IBrandRepository Brands { get; }
    public ISupplierRepository Suppliers { get; }
    public IWarehouseRepository Warehouses { get; }
    public IProductRepository Products { get; }
    public IPurchaseOrderRepository PurchaseOrders { get; }
    public IPurchaseOrderItemRepository PurchaseOrderItems { get; }
    public ISalesOrderRepository SalesOrders { get; }
    public ISalesOrderItemRepository SalesOrderItems { get; }
    public IInventoryTransactionRepository InventoryTransactions { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}
