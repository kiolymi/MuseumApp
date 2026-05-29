namespace MuseumApp.Services.Crud;

public sealed class CrudFieldSpec
{
    public required string PropertyName { get; init; }
    public required string Label { get; init; }
    public required CrudFieldKind Kind { get; init; }
    public FkReference? ForeignKey { get; init; }
    public bool IsKey { get; init; }
    public bool ReadOnlyOnEdit { get; init; }
    public bool IsNullable { get; init; }
    public StringValidationKind StringValidation { get; init; } = StringValidationKind.None;
    public int MaxLength { get; init; } = 255;
}
