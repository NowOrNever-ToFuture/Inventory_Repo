using HomeInventory.Application.Common.Interface.RepoInterfaces;
using HomeInventory.Application.Common.Interface.ServiceInterfaces;
using HomeInventory.Application.Features.Report.Dtos;

namespace HomeInventory.Application.Services;

public class ReportService(IUnitOfWork unitOfWork) : IReportService
{
    public async Task<List<ImportSummaryDto>> GetImportSummaryAsync(int year, int? month = null)
    {
        var summary = await unitOfWork.PurchaseOrders.GetImportSummaryAsync(year, month);

        return summary
            .Select(x => new ImportSummaryDto
            {
                Year = x.Year,
                Month = x.Month,
                TotalImportAmountVnd = x.TotalAmount
            })
            .ToList();
    }
}
