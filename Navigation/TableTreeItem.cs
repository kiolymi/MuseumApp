namespace MuseumApp.Navigation;

public sealed class TableTreeItem
{
    public required string Title { get; init; }
    public required TableId TableId { get; init; }
    public TableAccessLevel Access { get; init; }

    public override string ToString() => Title;
}
