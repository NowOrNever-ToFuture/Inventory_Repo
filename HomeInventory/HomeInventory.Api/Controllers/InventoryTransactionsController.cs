using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.InventoryTransaction.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>API quản lý giao dịch tồn kho.</summary>
[ApiController]
[Route("api/[controller]")]
public class InventoryTransactionsController(IInventoryTransactionService inventoryTransactionService) : ControllerBase
{
    /// <summary>Lấy danh sách giao dịch tồn kho có phân trang.</summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<InventoryTransactionResponseDto>>> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0) return BadRequest("pageIndex và pageSize phải lớn hơn 0.");
        var data = await inventoryTransactionService.GetAllAsync();
        var paged = await Pagination<InventoryTransactionResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>Lấy thông tin giao dịch tồn kho theo id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<InventoryTransactionResponseDto>> GetById(Guid id)
    {
        var item = await inventoryTransactionService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Tạo mới giao dịch tồn kho.</summary>
    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] InventoryTransactionRequestDto request)
    {
        var id = await inventoryTransactionService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Cập nhật giao dịch tồn kho theo id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] InventoryTransactionRequestDto request)
    {
        var updated = await inventoryTransactionService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>Xóa giao dịch tồn kho theo id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await inventoryTransactionService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
