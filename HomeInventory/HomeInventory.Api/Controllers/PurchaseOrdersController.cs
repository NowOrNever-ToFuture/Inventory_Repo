using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.PurchaseOrder.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>API quản lý đơn mua hàng.</summary>
[ApiController]
[Route("api/[controller]")]
public class PurchaseOrdersController(IPurchaseOrderService purchaseOrderService) : ControllerBase
{
    /// <summary>Lấy danh sách đơn mua có phân trang.</summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<PurchaseOrderResponseDto>>> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0) return BadRequest("pageIndex và pageSize phải lớn hơn 0.");
        var data = await purchaseOrderService.GetAllAsync();
        var paged = await Pagination<PurchaseOrderResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>Lấy thông tin đơn mua theo id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PurchaseOrderResponseDto>> GetById(Guid id)
    {
        var item = await purchaseOrderService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Tạo mới đơn mua.</summary>
    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] PurchaseOrderRequestDto request)
    {
        var id = await purchaseOrderService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Cập nhật đơn mua theo id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PurchaseOrderRequestDto request)
    {
        var updated = await purchaseOrderService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>Xóa đơn mua theo id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await purchaseOrderService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
