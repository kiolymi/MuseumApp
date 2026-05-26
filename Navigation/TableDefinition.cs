namespace MuseumApp.Navigation;

public sealed class TableDefinition
{
    public required TableId Id { get; init; }
    public required string Title { get; init; }
    public required string DbName { get; init; }
    public required TableAccessLevel Admin { get; init; }
    public required TableAccessLevel Curator { get; init; }
    public required TableAccessLevel Cashier { get; init; }

    public TableAccessLevel GetAccess(string role) => role switch
    {
        "admin_museum" => Admin,
        "curator_museum" => Curator,
        "cashier_museum" => Cashier,
        _ => TableAccessLevel.None
    };

    public bool CanRead(string role) => GetAccess(role) >= TableAccessLevel.Read;

    public bool CanWrite(string role) => GetAccess(role) >= TableAccessLevel.Write;

    public bool CanDelete(string role) => GetAccess(role) >= TableAccessLevel.Write;
}
