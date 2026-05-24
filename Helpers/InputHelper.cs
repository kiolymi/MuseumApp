using System.Globalization;

namespace MuseumApp.Helpers;

public static class InputHelper
{
    public static decimal ParseDecimal(string text)
    {
        var value = text.Trim();
        if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out var result))
            return result;
        if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
            return result;
        throw new FormatException("Неверный формат числа: " + text);
    }

    public static double? ParseNullableDouble(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        var value = text.Trim();
        if (double.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out var result))
            return result;
        if (double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
            return result;
        throw new FormatException("Неверный формат числа: " + text);
    }

    public static int ParseInt(string text)
    {
        var value = text.Trim();
        if (int.TryParse(value, NumberStyles.Integer, CultureInfo.CurrentCulture, out var result))
            return result;
        if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            return result;
        throw new FormatException("Неверный формат целого числа: " + text);
    }

    public static int? ParseNullableInt(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;
        return ParseInt(text);
    }
}
