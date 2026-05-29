using MuseumApp.Data.Entities;
using MuseumApp.Navigation;

namespace MuseumApp.Services.Crud;

public static class CrudRegistry
{
    private static readonly Dictionary<TableId, CrudTableSpec> Specs = Build().ToDictionary(s => s.TableId);

    public static bool TryGet(TableId id, out CrudTableSpec spec) => Specs.TryGetValue(id, out spec!);

    public static bool IsGeneric(TableId id) => Specs.ContainsKey(id);

    private static IEnumerable<CrudTableSpec> Build()
    {
        yield return Spec(TableId.Adresses, typeof(Adress), ["IdAddress"], "City",
            F("City", "Город", CrudFieldKind.String, StringValidationKind.SafeText, 45),
            F("Street", "Улица", CrudFieldKind.String, StringValidationKind.SafeText, 45),
            F("House", "Дом", CrudFieldKind.String, StringValidationKind.SafeText, 45),
            F("PostalCode", "Индекс", CrudFieldKind.NullableString, StringValidationKind.OptionalSafeText, 20));

        yield return Spec(TableId.Countries, typeof(Country), ["IdCountry"], "CountryName",
            F("CountryName", "Страна", CrudFieldKind.String));

        yield return Spec(TableId.Companies, typeof(Company), ["IdCompany"], "CompanyName",
            F("CompanyName", "Компания", CrudFieldKind.String, StringValidationKind.SafeText, 255),
            F("Inn", "ИНН", CrudFieldKind.NullableString, StringValidationKind.Inn, 12),
            F("LegalAddress", "Юр. адрес", CrudFieldKind.NullableString, StringValidationKind.OptionalSafeText, 255),
            F("ContactPhone", "Телефон", CrudFieldKind.NullableString, StringValidationKind.Phone, 45),
            F("ContactEmail", "Email", CrudFieldKind.NullableString, StringValidationKind.Email, 100));

        yield return Spec(TableId.Positions, typeof(Position), ["IdPosition"], "PositionName",
            F("PositionName", "Должность", CrudFieldKind.String),
            F("Description", "Описание", CrudFieldKind.NullableString),
            F("Salary", "Оклад", CrudFieldKind.NullableDecimal));

        yield return Spec(TableId.Materials, typeof(Material), ["IdMaterial"], "MaterialName",
            F("MaterialName", "Материал", CrudFieldKind.String),
            F("Description", "Описание", CrudFieldKind.NullableString));

        yield return Spec(TableId.Reasons, typeof(Reason), ["IdReason"], "ReasonDescription",
            F("ReasonDescription", "Причина", CrudFieldKind.String));

        yield return Spec(TableId.ExhibitConditions, typeof(ExhibitCondition), ["IdCondition"], "ConditionName",
            F("ConditionName", "Состояние", CrudFieldKind.String),
            F("Description", "Описание", CrudFieldKind.String));

        yield return Spec(TableId.Museums, typeof(Museum), ["IdMuseum"], "Name",
            F("Name", "Название", CrudFieldKind.String),
            Fk("IdDirector", "Директор", FkReference.Employee),
            Fk("IdAddress", "Адрес", FkReference.Address));

        yield return Spec(TableId.Branches, typeof(Branch), ["IdBranch"], "BranchName",
            F("BranchName", "Филиал", CrudFieldKind.String),
            Fk("IdMuseum", "Музей", FkReference.Museum),
            Fk("IdAddress", "Адрес", FkReference.Address),
            F("Phone", "Телефон", CrudFieldKind.NullableString, StringValidationKind.Phone, 45),
            Fk("IdResponsible", "Ответственный", FkReference.Employee));

        yield return Spec(TableId.Storages, typeof(Storage), ["IdStorage"], "StorageName",
            F("StorageName", "Хранилище", CrudFieldKind.String),
            Fk("IdBranch", "Филиал", FkReference.Branch),
            F("Temperature", "Температура", CrudFieldKind.NullableDecimal),
            F("Humidity", "Влажность", CrudFieldKind.NullableDecimal));

        yield return Spec(TableId.Shops, typeof(Shop), ["IdShop"], "ShopName",
            F("ShopName", "Магазин", CrudFieldKind.String),
            Fk("IdBranch", "Филиал", FkReference.Branch),
            F("Phone", "Телефон", CrudFieldKind.NullableString, StringValidationKind.Phone, 45),
            F("Email", "Email", CrudFieldKind.NullableString, StringValidationKind.Email, 100),
            F("WorkingHours", "Часы работы", CrudFieldKind.NullableString, StringValidationKind.OptionalSafeText, 100));

        yield return Spec(TableId.Products, typeof(Product), ["IdProduct"], "ProductName",
            F("ProductName", "Товар", CrudFieldKind.String),
            F("Description", "Описание", CrudFieldKind.NullableString),
            F("Price", "Цена", CrudFieldKind.Decimal),
            Fk("IdCompanySupplier", "Поставщик", FkReference.Company));

        yield return Spec(TableId.ExhibitMovements, typeof(ExhibitMovement), ["IdMovement"], "IdMovement",
            Fk("IdExhibit", "Экспонат", FkReference.Exhibit),
            Fk("IdResponsible", "Ответственный", FkReference.Employee),
            Fk("FromStorageId", "Из хранилища", FkReference.Storage),
            Fk("ToStorageId", "В хранилище", FkReference.Storage),
            F("MovementDate", "Дата", CrudFieldKind.NullableDateOnly),
            Fk("IdReason", "Причина", FkReference.Reason));

        yield return Spec(TableId.Restorations, typeof(Restoration), ["IdRestoration"], "IdRestoration",
            Fk("IdExhibit", "Экспонат", FkReference.Exhibit),
            Fk("IdRestorer", "Реставратор", FkReference.Employee),
            F("StartDate", "Начало", CrudFieldKind.DateTime),
            F("EndDate", "Окончание", CrudFieldKind.NullableDateTime),
            F("Cost", "Стоимость", CrudFieldKind.NullableDecimal),
            F("WorkDescription", "Описание работ", CrudFieldKind.NullableString));

        yield return Spec(TableId.Reviews, typeof(Review), ["IdReview"], "IdReview",
            Fk("IdVisitor", "Посетитель", FkReference.Visitor),
            Fk("IdExhibition", "Выставка", FkReference.Exhibition, nullable: true),
            F("Rating", "Оценка", CrudFieldKind.Int),
            F("Comment", "Комментарий", CrudFieldKind.NullableString),
            F("ReviewDate", "Дата отзыва", CrudFieldKind.DateOnly));

        yield return Spec(TableId.EventTickets, typeof(EventTicket), ["IdVisitor", "IdEvent"], "IdVisitor",
            KeyFk("IdVisitor", "Посетитель", FkReference.Visitor),
            KeyFk("IdEvent", "Мероприятие", FkReference.Event),
            F("PurchaseDate", "Дата покупки", CrudFieldKind.DateOnly),
            F("ActualPrice", "Цена", CrudFieldKind.Decimal));

        yield return Spec(TableId.Inventories, typeof(Inventory), ["IdShop", "IdProduct"], "IdShop",
            KeyFk("IdShop", "Магазин", FkReference.Shop),
            KeyFk("IdProduct", "Товар", FkReference.Product),
            F("Quantity", "Количество", CrudFieldKind.Int));
    }

    private static CrudTableSpec Spec(TableId id, Type entityType, string[] keys, string display,
        params CrudFieldSpec[] fields) => new()
    {
        TableId = id,
        EntityType = entityType,
        KeyProperties = keys,
        DisplayProperty = display,
        Fields = fields
    };

    private static CrudFieldSpec F(string prop, string label, CrudFieldKind kind,
        StringValidationKind stringValidation = StringValidationKind.None, int maxLength = 255) => new()
    {
        PropertyName = prop,
        Label = label,
        Kind = kind,
        StringValidation = stringValidation,
        MaxLength = maxLength
    };

    private static CrudFieldSpec Fk(string prop, string label, FkReference fk, bool nullable = false) => new()
    {
        PropertyName = prop,
        Label = label,
        Kind = CrudFieldKind.ForeignKey,
        ForeignKey = fk,
        IsNullable = nullable
    };

    private static CrudFieldSpec KeyFk(string prop, string label, FkReference fk) => new()
    {
        PropertyName = prop,
        Label = label,
        Kind = CrudFieldKind.ForeignKey,
        ForeignKey = fk,
        IsKey = true,
        ReadOnlyOnEdit = true
    };
}
