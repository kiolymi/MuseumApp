namespace MuseumApp.Navigation;

public sealed class TableTreeItem
{
    public required TableDefinition Definition { get; init; }

    public string SearchText => $"{Definition.Title} {Definition.DbName}";
}
