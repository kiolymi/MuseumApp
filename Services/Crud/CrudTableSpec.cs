using MuseumApp.Navigation;

namespace MuseumApp.Services.Crud;

public sealed class CrudTableSpec
{
    public required TableId TableId { get; init; }
    public required Type EntityType { get; init; }
    public required string[] KeyProperties { get; init; }
    public required string DisplayProperty { get; init; }
    public required IReadOnlyList<CrudFieldSpec> Fields { get; init; }
}
