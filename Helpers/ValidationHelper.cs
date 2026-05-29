using System.Text.RegularExpressions;

namespace MuseumApp.Helpers;

public static class ValidationHelper
{
    private static readonly string[] ForbiddenFragments =
    [
        "'", "\"", "--", ";", "<", ">", "<script", "script>", "select ", " drop ", "drop ", " insert ", " delete ", " update ", " union "
    ];

    private static readonly Regex PhoneRegex = new(@"^[\d\s+\-()]+$", RegexOptions.Compiled);
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public const int MaxPhoneLength = 45;
    public const int MaxEmailLength = 100;

    public static string? First(params string?[] checks)
    {
        foreach (var check in checks)
        {
            if (check != null)
            {
                return check;
            }
        }

        return null;
    }

    public static string? NotEmpty(string? text, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return $"Заполните поле «{fieldName}».";
        }

        return null;
    }

    public static string? MaxLen(string? text, int maxLength, string fieldName)
    {
        if (text != null && text.Trim().Length > maxLength)
        {
            return $"Поле «{fieldName}» — не более {maxLength} символов.";
        }

        return null;
    }

    public static string? NoForbiddenChars(string? text, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        var value = text.Trim();
        var lower = value.ToLowerInvariant();

        foreach (var fragment in ForbiddenFragments)
        {
            if (!lower.Contains(fragment, StringComparison.Ordinal))
            {
                continue;
            }

            return GetForbiddenMessage(fragment, fieldName);
        }

        return null;
    }

    public static string? SafeText(string? text, int maxLength, string fieldName)
    {
        string? error;

        error = NotEmpty(text, fieldName);
        if (error != null)
        {
            return error;
        }

        error = MaxLen(text, maxLength, fieldName);
        if (error != null)
        {
            return error;
        }

        error = NoForbiddenChars(text, fieldName);
        if (error != null)
        {
            return error;
        }

        return null;
    }

    public static string? OptionalSafeText(string? text, int maxLength, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        string? error;

        error = MaxLen(text, maxLength, fieldName);
        if (error != null)
        {
            return error;
        }

        error = NoForbiddenChars(text, fieldName);
        if (error != null)
        {
            return error;
        }

        return null;
    }

    public static string? Phone(string? text, string fieldName, bool required)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            if (required)
            {
                return $"Заполните поле «{fieldName}».";
            }

            return null;
        }

        var value = text.Trim();

        if (value.Length < 5)
        {
            return $"«{fieldName}» слишком короткий (минимум 5 символов).";
        }

        if (value.Length > MaxPhoneLength)
        {
            return $"«{fieldName}» — не более {MaxPhoneLength} символов.";
        }

        if (!PhoneRegex.IsMatch(value))
        {
            return $"«{fieldName}» может содержать только цифры, пробелы и символы + - ( ).";
        }

        return NoForbiddenChars(value, fieldName);
    }

    public static string? Email(string? text, string fieldName, bool required)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            if (required)
            {
                return $"Заполните поле «{fieldName}».";
            }

            return null;
        }

        var value = text.Trim();

        if (value.Length > MaxEmailLength)
        {
            return $"«{fieldName}» — не более {MaxEmailLength} символов.";
        }

        if (!EmailRegex.IsMatch(value))
        {
            return $"«{fieldName}» имеет неверный формат (пример: user@mail.ru).";
        }

        return NoForbiddenChars(value, fieldName);
    }

    public static string? Inn(string? text, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        var value = text.Trim();

        foreach (var ch in value)
        {
            if (!char.IsDigit(ch))
            {
                return $"«{fieldName}» должен содержать только цифры.";
            }
        }

        if (value.Length != 10 && value.Length != 12)
        {
            return $"«{fieldName}» должен быть 10 или 12 цифр.";
        }

        return NoForbiddenChars(value, fieldName);
    }

    public static string? Combo(object? selectedValue, string fieldName)
    {
        if (selectedValue == null)
        {
            return $"Выберите «{fieldName}».";
        }

        return null;
    }

    public static string? Dates(DateTime? start, DateTime? end)
    {
        if (!start.HasValue)
        {
            return "Укажите дату начала.";
        }

        if (!end.HasValue)
        {
            return "Укажите дату окончания.";
        }

        if (start > end)
        {
            return "Дата начала не может быть позже даты окончания.";
        }

        return null;
    }

    public static string? VisitDate(DateTime? date)
    {
        if (!date.HasValue)
        {
            return "Укажите дату посещения.";
        }

        return null;
    }

    public static string? BirthDate(DateTime? date)
    {
        if (!date.HasValue)
        {
            return "Укажите дату рождения.";
        }

        return null;
    }

    public static string? NonNegative(decimal value, string fieldName)
    {
        if (value < 0)
        {
            return $"«{fieldName}» не может быть отрицательным.";
        }

        return null;
    }

    public static string? Positive(int value, string fieldName)
    {
        if (value <= 0)
        {
            return $"«{fieldName}» должно быть больше 0.";
        }

        return null;
    }

    public static string? DiscountPercent(double? value)
    {
        if (!value.HasValue)
        {
            return null;
        }

        if (value < 0 || value > 100)
        {
            return "Скидка должна быть от 0 до 100.";
        }

        return null;
    }

    private static string GetForbiddenMessage(string fragment, string fieldName)
    {
        switch (fragment)
        {
            case "'":
            case "\"":
                return $"Поле «{fieldName}» не должно содержать кавычки.";
            case "--":
            case ";":
                return $"Поле «{fieldName}» содержит запрещённые символы SQL (-- или ;).";
            case "<":
            case ">":
            case "<script":
            case "script>":
                return $"Поле «{fieldName}» не должно содержать HTML-теги.";
            case "select ":
            case " drop ":
            case "drop ":
            case " insert ":
            case " delete ":
            case " update ":
            case " union ":
                return $"Поле «{fieldName}» содержит запрещённые SQL-команды.";
            default:
                return $"Поле «{fieldName}» содержит недопустимые символы.";
        }
    }
}
