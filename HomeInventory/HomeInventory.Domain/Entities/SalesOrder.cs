using HomeInventory.Domain.Common;
using HomeInventory.Domain.Enum;

namespace HomeInventory.Domain.Entities;

public class SalesOrder : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Draft;

    public ICollection<SalesOrderItem> Items { get; set; } = new List<SalesOrderItem>();
}
