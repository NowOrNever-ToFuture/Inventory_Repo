using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Exceptions;
using HomeInventory.Application.Common.Exceptions.Entities;
using HomeInventory.Application.Common.Extensions;
using HomeInventory.Application.Features.PurchaseOrder.Dtos;
using HomeInventory.Domain.Entities;
using HomeInventory.Domain.Enum;

namespace HomeInventory.Application.Services;

public class PurchaseOrderService(IUnitOfWork unitOfWork) : IPurchaseOrderService
{
    public async Task<List<PurchaseOrderResponseDto>> GetAllAsync()
    {
        var entities = await unitOfWork.PurchaseOrders.GetAllAsync();
        var items = await unitOfWork.PurchaseOrderItems.GetByPurchaseOrderIdsAsync(entities.Select(x => x.Id));
        return entities.Select(x => Map(x, items)).ToList();
    }

    public async Task<PurchaseOrderResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await unitOfWork.PurchaseOrders.GetByIdAsync(id);
        if (entity is null) return null;

        var items = await unitOfWork.PurchaseOrderItems.GetByPurchaseOrderIdsAsync([id]);
        return Map(entity, items);
    }

    public async Task<PurchaseOrderResponseDto> CreateAsync(PurchaseOrderRequestDto request)
    {
        if (request.Items.Count == 0)
        {
            throw new ValidationException(new Dictionary<string, string[]>
            {
                ["Items"] = ["Danh sách hàng nhập không được rỗng."]
            });
        }

        var orderDate = DateTime.UtcNow;
        var code = string.IsNullOrWhiteSpace(request.Code)
            ? $"PO-{orderDate:yyyyMMddHHmmss}"
            : request.Code.Trim();

        var subTotal = request.Items.Sum(x => x.Quantity * x.UnitCost);

        var entity = new PurchaseOrder
        {
            Code = code,
            OrderDate = orderDate,
            Status = OrderStatus.Completed,
            SupplierId = request.SupplierId,
            SubTotalAmount = subTotal,
            DiscountAmount = 0,
            TotalAmount = subTotal
        };

        await unitOfWork.PurchaseOrders.AddAsync(entity);

        foreach (var item in request.Items)
        {
            var normalizedModel = item.Model.NormalizeKey();
            var existingProduct = await unitOfWork.Products.GetByModelNormalizedAsync(normalizedModel);

            if (existingProduct is null)
            {
                existingProduct = new Product
                {
                    Model = item.Model.Trim(),
                    ModelNormalized = normalizedModel,
                    Name = item.Name,
                    Unit = item.Unit,
                    CategoryId = item.CategoryId,
                    BrandId = item.BrandId,
                    StockQuantity = item.Quantity,
                    ImportPrice = item.UnitCost
                };

                await unitOfWork.Products.AddAsync(existingProduct);
            }
            else
            {
                existingProduct.Name = item.Name;
                existingProduct.Unit = item.Unit;
                existingProduct.CategoryId = item.CategoryId;
                existingProduct.BrandId = item.BrandId;
                existingProduct.ImportPrice = item.UnitCost;
                existingProduct.StockQuantity += item.Quantity;
                existingProduct.UpdatedAtUtc = DateTime.UtcNow;
                await unitOfWork.Products.UpdateAsync(existingProduct);
            }

            await unitOfWork.PurchaseOrderItems.AddAsync(new PurchaseOrderItem
            {
                PurchaseOrderId = entity.Id,
                ProductId = existingProduct.Id,
                Quantity = item.Quantity,
                UnitCost = item.UnitCost,
                LineTotal = item.Quantity * item.UnitCost
            });
        }

        await unitOfWork.SaveChangesAsync();
        var items = await unitOfWork.PurchaseOrderItems.GetByPurchaseOrderIdsAsync([entity.Id]);
        return Map(entity, items);
    }

    public async Task<PurchaseOrderResponseDto> UpdateAsync(Guid id, PurchaseOrderRequestDto request)
    {
        var entity = await unitOfWork.PurchaseOrders.GetByIdAsync(id);
        if (entity is null) throw new PurchaseOrderNotFoundException(id);

        entity.SupplierId = request.SupplierId;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.PurchaseOrders.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        var items = await unitOfWork.PurchaseOrderItems.GetByPurchaseOrderIdsAsync([entity.Id]);
        return Map(entity, items);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await unitOfWork.PurchaseOrders.GetByIdAsync(id);
        if (entity is null) return false;

        await unitOfWork.PurchaseOrders.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private static PurchaseOrderResponseDto Map(PurchaseOrder entity, List<PurchaseOrderItem> allItems) => new()
    {
        Id = entity.Id,
        Code = entity.Code,
        OrderDate = entity.OrderDate,
        SupplierId = entity.SupplierId,
        TotalAmount = entity.TotalAmount,
        Items = allItems.Where(i => i.PurchaseOrderId == entity.Id).Select(x => new PurchaseOrderResponseItemDto
        {
            ProductId = x.ProductId,
            Quantity = x.Quantity,
            UnitCost = x.UnitCost,
            LineTotal = x.LineTotal
        }).ToList()
    };
}
