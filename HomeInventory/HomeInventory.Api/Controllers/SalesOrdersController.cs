using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.SalesOrder.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>API quản lý đơn bán hàng.</summary>
[ApiController]
[Route("api/[controller]")]
public class SalesOrdersController(ISalesOrderService salesOrderService) : ControllerBase
{
    /// <summary>Lấy danh sách đơn bán có phân trang.</summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<SalesOrderResponseDto>>> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0) return BadRequest("pageIndex và pageSize phải lớn hơn 0.");
        var data = await salesOrderService.GetAllAsync();
        var paged = await Pagination<SalesOrderResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>Lấy thông tin đơn bán theo id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SalesOrderResponseDto>> GetById(Guid id)
    {
        var item = await salesOrderService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Tạo mới đơn bán.</summary>
    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] SalesOrderRequestDto request)
    {
        var id = await salesOrderService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Cập nhật đơn bán theo id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SalesOrderRequestDto request)
    {
        var updated = await salesOrderService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>Xóa đơn bán theo id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await salesOrderService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
