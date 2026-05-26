using Microsoft.EntityFrameworkCore;
using MuseumApp.Data;

namespace MuseumApp.Helpers;

public static class SessionUser
{
    public static string Login { get; set; } = "";
    public static string Role { get; set; } = "";
    public static string Password { get; set; } = "";

    public static string GetConnectionString()
    {
        return $"Host=localhost;Port=5433;Database=museum;Username={Login};Password={Password}";
    }

    /// <summary>
    /// Определяет роль приложения по членству пользователя в ролях PostgreSQL.
    /// </summary>
    public static string ResolveRoleFromDatabase()
    {
        using var context = new MuseumDbContext();
        var connection = context.Database.GetDbConnection();
        if (connection.State != System.Data.ConnectionState.Open)
            connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT r.rolname
            FROM pg_auth_members am
            JOIN pg_roles m ON m.oid = am.member
            JOIN pg_roles r ON r.oid = am.roleid
            WHERE m.rolname = current_user
              AND r.rolname IN ('admin_museum', 'curator_museum', 'cashier_museum')
            ORDER BY CASE r.rolname
                WHEN 'admin_museum' THEN 1
                WHEN 'curator_museum' THEN 2
                WHEN 'cashier_museum' THEN 3
                ELSE 4
            END
            LIMIT 1
            """;

        return command.ExecuteScalar()?.ToString() ?? "";
    }
}
