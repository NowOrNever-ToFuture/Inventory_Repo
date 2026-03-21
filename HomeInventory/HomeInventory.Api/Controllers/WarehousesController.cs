using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.Warehouse.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>API quản lý kho hàng.</summary>
[ApiController]
[Route("api/[controller]")]
public class WarehousesController(IWarehouseService warehouseService) : ControllerBase
{
    /// <summary>Lấy danh sách kho có phân trang.</summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<WarehouseResponseDto>>> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0) return BadRequest("pageIndex và pageSize phải lớn hơn 0.");
        var data = await warehouseService.GetAllAsync();
        var paged = await Pagination<WarehouseResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>Lấy thông tin kho theo id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WarehouseResponseDto>> GetById(Guid id)
    {
        var item = await warehouseService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Tạo mới kho.</summary>
    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] WarehouseRequestDto request)
    {
        var id = await warehouseService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Cập nhật kho theo id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] WarehouseRequestDto request)
    {
        var updated = await warehouseService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>Xóa kho theo id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await warehouseService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
