using ClosedXML.Excel;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Features.Report.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

/// <summary>
/// API báo cáo tổng hợp nhập hàng theo tháng/năm.
/// </summary>
[ApiController]
[Route("api/reports")]
public class ReportsController(IReportService reportService) : ControllerBase
{
    /// <summary>
    /// Lấy báo cáo tổng tiền nhập theo năm và tháng (tùy chọn).
    /// </summary>
    [HttpGet("import-summary")]
    public async Task<ActionResult<List<ImportSummaryDto>>> GetImportSummary([FromQuery] int year, [FromQuery] int? month)
    {
        if (year <= 0) return BadRequest("year là bắt buộc và phải lớn hơn 0.");
        if (month.HasValue && (month < 1 || month > 12)) return BadRequest("month phải nằm trong khoảng 1..12.");

        var data = await reportService.GetImportSummaryAsync(year, month);
        return Ok(data);
    }

    /// <summary>
    /// Xuất báo cáo tổng tiền nhập theo năm và tháng (tùy chọn) ra file Excel.
    /// </summary>
    [HttpGet("import-summary/excel")]
    public async Task<IActionResult> ExportImportSummaryExcel([FromQuery] int year, [FromQuery] int? month)
    {
        if (year <= 0) return BadRequest("year là bắt buộc và phải lớn hơn 0.");
        if (month.HasValue && (month < 1 || month > 12)) return BadRequest("month phải nằm trong khoảng 1..12.");

        var data = await reportService.GetImportSummaryAsync(year, month);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("ImportSummary");

        worksheet.Cell(1, 1).Value = "Year";
        worksheet.Cell(1, 2).Value = "Month";
        worksheet.Cell(1, 3).Value = "TotalImportAmountVnd";

        for (var i = 0; i < data.Count; i++)
        {
            var row = i + 2;
            worksheet.Cell(row, 1).Value = data[i].Year;
            worksheet.Cell(row, 2).Value = data[i].Month;
            worksheet.Cell(row, 3).Value = data[i].TotalImportAmountVnd;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        var fileName = $"import-summary-{year}" + (month.HasValue ? $"-{month.Value:00}" : string.Empty) + ".xlsx";
        return File(
            stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileName);
    }
}
