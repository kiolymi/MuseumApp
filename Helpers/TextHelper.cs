namespace MuseumApp.Helpers;

public static class TextHelper
{
    public static string? TrimOrNull(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        return text.Trim();
    }

    public static string TrimOrEmpty(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        return text.Trim();
    }
}
