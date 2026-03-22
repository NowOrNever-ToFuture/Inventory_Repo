using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.Brand.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>API quản lý hãng sản phẩm.</summary>
[ApiController]
[Route("api/[controller]")]
public class BrandsController(IBrandService brandService) : ControllerBase
{
    /// <summary>Lấy danh sách hãng có phân trang.</summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<BrandResponseDto>>> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0) return BadRequest("pageIndex và pageSize phải lớn hơn 0.");
        var data = await brandService.GetAllAsync();
        var paged = await Pagination<BrandResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>Gợi ý hãng theo từ khóa (hỗ trợ tiếng Việt, không phân biệt hoa thường).</summary>
    [HttpGet("suggest")]
    public async Task<ActionResult<List<string>>> Suggest([FromQuery] string q, [FromQuery] int limit = 10)
    {
        if (string.IsNullOrWhiteSpace(q)) return BadRequest("q không được để trống.");
        if (limit <= 0) return BadRequest("limit phải lớn hơn 0.");

        var values = await brandService.SuggestAsync(q, limit);
        return Ok(values);
    }

    /// <summary>Lấy hãng theo id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BrandResponseDto>> GetById(Guid id)
    {
        var item = await brandService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Tạo mới hãng.</summary>
    [HttpPost]
    public async Task<ActionResult<BrandResponseDto>> Create([FromBody] BrandRequestDto request)
    {
        var created = await brandService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Cập nhật hãng theo id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<BrandResponseDto>> Update(Guid id, [FromBody] BrandRequestDto request)
    {
        var updated = await brandService.UpdateAsync(id, request);
        return Ok(updated);
    }

    /// <summary>Xóa hãng theo id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await brandService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
