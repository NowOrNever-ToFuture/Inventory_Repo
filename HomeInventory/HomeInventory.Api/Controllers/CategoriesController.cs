using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.Category.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>
/// API quản lý danh mục sản phẩm.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    /// <summary>
    /// Lấy danh sách danh mục có phân trang.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<CategoryResponseDto>>> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0)
            return BadRequest("pageIndex và pageSize phải lớn hơn 0.");

        var data = await categoryService.GetAllAsync();
        var paged = await Pagination<CategoryResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>
    /// Lấy thông tin danh mục theo id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponseDto>> GetById(Guid id)
    {
        var item = await categoryService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>
    /// Tạo mới danh mục.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] CategoryRequestDto request)
    {
        var id = await categoryService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>
    /// Cập nhật danh mục theo id.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CategoryRequestDto request)
    {
        var updated = await categoryService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>
    /// Xóa danh mục theo id.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await categoryService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
