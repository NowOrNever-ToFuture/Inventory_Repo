using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Exceptions;
using HomeInventory.Application.Common.Exceptions.Entities;
using HomeInventory.Application.Features.SalesOrder.Dtos;
using HomeInventory.Domain.Entities;
using HomeInventory.Domain.Enum;

namespace HomeInventory.Application.Services;

public class SalesOrderService(IUnitOfWork unitOfWork) : ISalesOrderService
{
    public async Task<List<SalesOrderResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.SalesOrders.GetAllAsync();
        var items = await unitOfWork.SalesOrderItems.GetBySalesOrderIdsAsync(entities.Select(x => x.Id));
        return entities.Select(x => Map(x, items)).ToList();
    }

    public async Task<SalesOrderResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.SalesOrders.GetByIdAsync(id);
        if (entity is null) return null;

        var items = await unitOfWork.SalesOrderItems.GetBySalesOrderIdsAsync([id]);
        return Map(entity, items);
    }

    public async Task<SalesOrderResponseDto> CreateAsync(SalesOrderRequestDto request)
    {
        if (request.Items.Count == 0)
        {
            throw new ValidationException(new Dictionary<string, string[]>
            {
                ["Items"] = ["Danh sách hàng xuất không được rỗng."]
            });
        }

        var orderDate = DateTime.UtcNow;
        var code = string.IsNullOrWhiteSpace(request.Code)
            ? $"SO-{orderDate:yyyyMMddHHmmss}"
            : request.Code.Trim();

        var entity = new SalesOrder
        {
            Code = code,
            OrderDate = orderDate,
            Status = OrderStatus.Completed
        };

        await unitOfWork.SalesOrders.AddAsync(entity);

        var productIds = request.Items.Select(x => x.ProductId);
        var productsById = await unitOfWork.Products.GetByIdsAsDictionaryAsync(productIds);

        foreach (var item in request.Items)
        {
            if (!productsById.TryGetValue(item.ProductId, out var product))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    ["ProductId"] = [$"Không tìm thấy sản phẩm {item.ProductId}."]
                });
            }

            if (product.StockQuantity < item.Quantity)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    ["StockQuantity"] = [$"Sản phẩm {product.Model} không đủ tồn kho."]
                });
            }

            product.StockQuantity -= item.Quantity;
            product.UpdatedAtUtc = DateTime.UtcNow;
            await unitOfWork.Products.UpdateAsync(product);

            await unitOfWork.SalesOrderItems.AddAsync(new SalesOrderItem
            {
                SalesOrderId = entity.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            });
        }

        await unitOfWork.SaveChangesAsync();
        var items = await unitOfWork.SalesOrderItems.GetBySalesOrderIdsAsync([entity.Id]);
        return Map(entity, items);
    }

    public async Task<SalesOrderResponseDto> UpdateAsync(Guid id, SalesOrderRequestDto request)
    {
        var entity = await unitOfWork.SalesOrders.GetByIdAsync(id);
        if (entity is null) throw new SalesOrderNotFoundException(id);

        if (!string.IsNullOrWhiteSpace(request.Code))
            entity.Code = request.Code.Trim();
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.SalesOrders.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        var items = await unitOfWork.SalesOrderItems.GetBySalesOrderIdsAsync([entity.Id]);
        return Map(entity, items);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.SalesOrders.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.SalesOrders.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static SalesOrderResponseDto Map(SalesOrder entity, List<SalesOrderItem> allItems) => new()
    {
        Id = entity.Id,
        Code = entity.Code,
        OrderDate = entity.OrderDate,
        Items = allItems.Where(i => i.SalesOrderId == entity.Id).Select(x => new SalesOrderResponseItemDto
        {
            ProductId = x.ProductId,
            Quantity = x.Quantity
        }).ToList()
    };
}
