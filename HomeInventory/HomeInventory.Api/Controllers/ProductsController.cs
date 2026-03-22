using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.Product.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>API quản lý sản phẩm.</summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    /// <summary>Lấy danh sách sản phẩm trong kho có phân trang và lọc theo hãng/loại/model.</summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductResponseDto>>> GetAll(
        [FromQuery] Guid? brandId,
        [FromQuery] Guid? categoryId,
        [FromQuery] string? model,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0) return BadRequest("pageIndex và pageSize phải lớn hơn 0.");
        var data = await productService.GetAllAsync(brandId, categoryId, model);
        var paged = await Pagination<ProductResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>Gợi ý model sản phẩm theo từ khóa (hỗ trợ tiếng Việt, không phân biệt hoa thường).</summary>
    [HttpGet("suggest")]
    public async Task<ActionResult<List<ProductResponseDto>>> Suggest([FromQuery] string q, [FromQuery] int limit = 10)
    {
        if (string.IsNullOrWhiteSpace(q)) return BadRequest("q không được để trống.");
        if (limit <= 0) return BadRequest("limit phải lớn hơn 0.");

        var values = await productService.SuggestAsync(q, limit);
        return Ok(values);
    }

    /// <summary>Lấy thông tin sản phẩm theo id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(Guid id)
    {
        var item = await productService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Tạo mới sản phẩm.</summary>
    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> Create([FromBody] ProductRequestDto request)
    {
        var created = await productService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Cập nhật sản phẩm theo id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductResponseDto>> Update(Guid id, [FromBody] ProductRequestDto request)
    {
        var updated = await productService.UpdateAsync(id, request);
        return Ok(updated);
    }

    /// <summary>Xóa sản phẩm theo id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await productService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
