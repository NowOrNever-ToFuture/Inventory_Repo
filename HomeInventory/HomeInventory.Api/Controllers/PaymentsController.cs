using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Application.Features.Payment.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>API quản lý thanh toán.</summary>
[ApiController]
[Route("api/[controller]")]
public class PaymentsController(IPaymentService paymentService) : ControllerBase
{
    /// <summary>Lấy danh sách thanh toán có phân trang.</summary>
    [HttpGet]
    public async Task<ActionResult<Pagination<PaymentResponseDto>>> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        if (pageIndex <= 0 || pageSize <= 0) return BadRequest("pageIndex và pageSize phải lớn hơn 0.");
        var data = await paymentService.GetAllAsync();
        var paged = await Pagination<PaymentResponseDto>.ToPagedList(data.AsQueryable(), pageIndex, pageSize);
        return Ok(paged);
    }

    /// <summary>Lấy thông tin thanh toán theo id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PaymentResponseDto>> GetById(Guid id)
    {
        var item = await paymentService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Tạo mới thanh toán.</summary>
    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] PaymentRequestDto request)
    {
        var id = await paymentService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Cập nhật thanh toán theo id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PaymentRequestDto request)
    {
        var updated = await paymentService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>Xóa thanh toán theo id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await paymentService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
