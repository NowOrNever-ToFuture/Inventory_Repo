using HomeInventory.Application.Features.Report.Dtos;

namespace HomeInventory.Application.Common.Interface.ServiceInterfaces;

public interface IReportService
{
    Task<List<ImportSummaryDto>> GetImportSummaryAsync(int year, int? month = null);
}
