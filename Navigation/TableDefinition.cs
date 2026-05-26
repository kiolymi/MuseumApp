namespace MuseumApp.Navigation;

public sealed class TableDefinition
{
    public required TableId Id { get; init; }
    public required string Title { get; init; }
    public required Func<string, TableAccessLevel> GetAccess { get; init; }
}
