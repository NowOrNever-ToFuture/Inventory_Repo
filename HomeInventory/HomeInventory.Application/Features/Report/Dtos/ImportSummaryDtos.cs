namespace HomeInventory.Application.Features.Report.Dtos;

public class ImportSummaryDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalImportAmountVnd { get; set; }
}
