namespace MuseumApp.Helpers;

public static class ValidationHelper
{
    public static string? First(params string?[] checks)
    {
        foreach (var check in checks)
        {
            if (check != null)
                return check;
        }

        return null;
    }

    public static string? NotEmpty(string? text, string fieldName)
    {
        return string.IsNullOrWhiteSpace(text)
            ? $"Заполните поле «{fieldName}»."
            : null;
    }

    public static string? MaxLen(string? text, int maxLength, string fieldName)
    {
        if (text != null && text.Trim().Length > maxLength)
            return $"Поле «{fieldName}» — не более {maxLength} символов.";
        return null;
    }

    public static string? Combo(object? selectedValue, string fieldName)
    {
        return selectedValue == null
            ? $"Выберите «{fieldName}»."
            : null;
    }

    public static string? Dates(DateTime? start, DateTime? end)
    {
        if (!start.HasValue)
            return "Укажите дату начала.";
        if (!end.HasValue)
            return "Укажите дату окончания.";
        if (start > end)
            return "Дата начала не может быть позже даты окончания.";
        return null;
    }

    public static string? VisitDate(DateTime? date)
    {
        return !date.HasValue ? "Укажите дату посещения." : null;
    }

    public static string? BirthDate(DateTime? date)
    {
        return !date.HasValue ? "Укажите дату рождения." : null;
    }

    public static string? NonNegative(decimal value, string fieldName)
    {
        return value < 0 ? $"«{fieldName}» не может быть отрицательным." : null;
    }

    public static string? Positive(int value, string fieldName)
    {
        return value <= 0 ? $"«{fieldName}» должно быть больше 0." : null;
    }

    public static string? DiscountPercent(double? value)
    {
        if (!value.HasValue)
            return null;
        if (value < 0 || value > 100)
            return "Скидка должна быть от 0 до 100.";
        return null;
    }
}
