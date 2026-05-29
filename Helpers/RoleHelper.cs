namespace MuseumApp.Helpers;

public static class RoleHelper
{
    private static readonly HashSet<string> KnownLogins = new(StringComparer.OrdinalIgnoreCase)
    {
        "ivanov", "admin_museum",
        "petrova", "curator_museum",
        "pavlov", "cashier_museum"
    };

    public static bool IsKnownLogin(string login)
    {
        return KnownLogins.Contains(login.Trim());
    }

    public static string ResolveRole(string login)
    {
        var normalized = login.Trim().ToLowerInvariant();

        switch (normalized)
        {
            case "ivanov":
            case "admin_museum":
                return "admin_museum";
            case "petrova":
            case "curator_museum":
                return "curator_museum";
            case "pavlov":
            case "cashier_museum":
                return "cashier_museum";
            default:
                return normalized;
        }
    }

    public static string GetDisplayName(string role)
    {
        switch (role)
        {
            case "admin_museum":
                return "Администратор";
            case "curator_museum":
                return "Куратор";
            case "cashier_museum":
                return "Кассир";
            default:
                return role;
        }
    }
}
