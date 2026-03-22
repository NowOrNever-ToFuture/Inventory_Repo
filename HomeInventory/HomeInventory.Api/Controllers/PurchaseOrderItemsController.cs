using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.PurchaseOrderItem.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>API quản lý chi tiết đơn mua.</summary>
[ApiController]
[Route("api/[controller]")]
public class PurchaseOrderItemsController(IPurchaseOrderItemService purchaseOrderItemService) : ControllerBase
{
    /// <summary>Lấy danh sách chi tiết đơn mua có phân trang.</summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<PurchaseOrderItemResponseDto>>> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0) return BadRequest("pageIndex và pageSize phải lớn hơn 0.");
        var data = await purchaseOrderItemService.GetAllAsync();
        var paged = await Pagination<PurchaseOrderItemResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>Lấy thông tin chi tiết đơn mua theo id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PurchaseOrderItemResponseDto>> GetById(Guid id)
    {
        var item = await purchaseOrderItemService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Tạo mới chi tiết đơn mua.</summary>
    [HttpPost]
    public async Task<ActionResult<PurchaseOrderItemResponseDto>> Create([FromBody] PurchaseOrderItemRequestDto request)
    {
        var created = await purchaseOrderItemService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Cập nhật chi tiết đơn mua theo id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PurchaseOrderItemResponseDto>> Update(Guid id, [FromBody] PurchaseOrderItemRequestDto request)
    {
        var updated = await purchaseOrderItemService.UpdateAsync(id, request);
        return Ok(updated);
    }

    /// <summary>Xóa chi tiết đơn mua theo id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await purchaseOrderItemService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
