using Microsoft.EntityFrameworkCore;

namespace MuseumApp.Helpers;

public static class DbExceptionHelper
{
    public static string GetMessage(Exception ex)
    {
        if (ex is DbUpdateException dbEx)
        {
            var detail = dbEx.InnerException?.Message;
            if (!string.IsNullOrWhiteSpace(detail))
                return detail;
        }

        var parts = new List<string>();
        for (var current = ex; current != null; current = current.InnerException)
        {
            if (!string.IsNullOrWhiteSpace(current.Message))
                parts.Add(current.Message);
        }

        return string.Join(Environment.NewLine, parts.Distinct());
    }
}
