using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.Supplier.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>
/// API quản lý nhà cung cấp.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SuppliersController(ISupplierService supplierService) : ControllerBase
{
    /// <summary>Lấy danh sách nhà cung cấp có phân trang.</summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<SupplierResponseDto>>> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0) return BadRequest("pageIndex và pageSize phải lớn hơn 0.");
        var data = await supplierService.GetAllAsync();
        var paged = await Pagination<SupplierResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>Gợi ý nhà cung cấp theo từ khóa (hỗ trợ tiếng Việt, không phân biệt hoa thường).</summary>
    [HttpGet("suggest")]
    public async Task<ActionResult<List<SupplierResponseDto>>> Suggest([FromQuery] string q, [FromQuery] int limit = 10)
    {
        if (string.IsNullOrWhiteSpace(q)) return BadRequest("q không được để trống.");
        if (limit <= 0) return BadRequest("limit phải lớn hơn 0.");

        var values = await supplierService.SuggestAsync(q, limit);
        return Ok(values);
    }

    /// <summary>Lấy thông tin nhà cung cấp theo id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SupplierResponseDto>> GetById(Guid id)
    {
        var item = await supplierService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Tạo mới nhà cung cấp.</summary>
    [HttpPost]
    public async Task<ActionResult<SupplierResponseDto>> Create([FromBody] SupplierRequestDto request)
    {
        var created = await supplierService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Cập nhật nhà cung cấp theo id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SupplierResponseDto>> Update(Guid id, [FromBody] SupplierRequestDto request)
    {
        var updated = await supplierService.UpdateAsync(id, request);
        return Ok(updated);
    }

    /// <summary>Xóa nhà cung cấp theo id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await supplierService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
