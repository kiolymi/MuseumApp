namespace MuseumApp.Helpers;

public static class RoleHelper
{
    public static string ResolveRole(string login)
    {
        return login.Trim().ToLowerInvariant() switch
        {
            "ivanov" or "admin_museum" => "admin_museum",
            "petrova" or "curator_museum" => "curator_museum",
            "pavlov" or "cashier_museum" => "cashier_museum",
            _ => login.Trim().ToLowerInvariant()
        };
    }

    public static string GetDisplayName(string role) => role switch
    {
        "admin_museum" => "Администратор",
        "curator_museum" => "Куратор",
        "cashier_museum" => "Кассир",
        _ => role
    };
}
