using System.Globalization;

namespace HomeInventory.Application.Common.Extensions;

public static class CurrencyExtensions
{
    private static readonly CultureInfo ViVnCulture = CultureInfo.GetCultureInfo("vi-VN");

    public static string ToVnd(this decimal amount)
    {
        return string.Format(ViVnCulture, "{0:#,0} ₫", amount);
    }

    public static string ToVnd(this decimal? amount)
    {
        return amount.HasValue ? amount.Value.ToVnd() : "0 ₫";
    }
}
