using Microsoft.Extensions.DependencyInjection;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Services;

namespace HomeInventory.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
        services.AddScoped<IPurchaseOrderItemService, PurchaseOrderItemService>();
        services.AddScoped<ISalesOrderService, SalesOrderService>();
        services.AddScoped<ISalesOrderItemService, SalesOrderItemService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();

        return services;
    }
}
