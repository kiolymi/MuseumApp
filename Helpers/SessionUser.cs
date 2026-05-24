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
}
