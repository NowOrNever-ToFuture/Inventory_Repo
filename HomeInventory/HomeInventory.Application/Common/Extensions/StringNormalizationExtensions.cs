namespace HomeInventory.Application.Common.Extensions;

public static class StringNormalizationExtensions
{
    public static string NormalizeKey(this string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : value.Trim().ToLowerInvariant();
    }
}
