using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.SalesOrderItem.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>API quản lý chi tiết đơn bán.</summary>
[ApiController]
[Route("api/[controller]")]
public class SalesOrderItemsController(ISalesOrderItemService salesOrderItemService) : ControllerBase
{
    /// <summary>Lấy danh sách chi tiết đơn bán có phân trang.</summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<SalesOrderItemResponseDto>>> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0) return BadRequest("pageIndex và pageSize phải lớn hơn 0.");
        var data = await salesOrderItemService.GetAllAsync();
        var paged = await Pagination<SalesOrderItemResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>Lấy thông tin chi tiết đơn bán theo id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SalesOrderItemResponseDto>> GetById(Guid id)
    {
        var item = await salesOrderItemService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Tạo mới chi tiết đơn bán.</summary>
    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] SalesOrderItemRequestDto request)
    {
        var id = await salesOrderItemService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Cập nhật chi tiết đơn bán theo id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SalesOrderItemRequestDto request)
    {
        var updated = await salesOrderItemService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>Xóa chi tiết đơn bán theo id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await salesOrderItemService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
