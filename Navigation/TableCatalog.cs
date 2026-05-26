namespace MuseumApp.Navigation;

public static class TableCatalog
{
  private static readonly TableAccessLevel A = TableAccessLevel.Write;
  private static readonly TableAccessLevel Cw = TableAccessLevel.Write;
  private static readonly TableAccessLevel Cr = TableAccessLevel.Read;
  private static readonly TableAccessLevel N = TableAccessLevel.None;

  public static IReadOnlyList<TableDefinition> All { get; } =
  [
    Def(TableId.Adresses, "Адреса", "adresses", A, N, N),
    Def(TableId.Countries, "Страны", "countries", A, N, N),
    Def(TableId.Materials, "Материалы", "materials", A, N, N),
    Def(TableId.Positions, "Должности", "positions", A, N, N),
    Def(TableId.Privileges, "Льготы", "privileges", A, N, Cw),
    Def(TableId.Reasons, "Причины перемещений", "reasons", A, N, N),
    Def(TableId.ExhibitConditions, "Состояния экспонатов", "exhibit_conditions", A, Cw, N),
    Def(TableId.Authors, "Авторы", "authors", A, Cw, N),
    Def(TableId.Employees, "Сотрудники", "employees", A, N, N),
    Def(TableId.Museum, "Музеи", "museum", A, N, N),
    Def(TableId.Branches, "Филиалы", "branches", A, N, N),
    Def(TableId.Storages, "Хранилища", "storages", A, N, N),
    Def(TableId.Collections, "Коллекции", "collections", A, Cw, N),
    Def(TableId.Exhibits, "Экспонаты", "exhibits", A, Cw, N),
    Def(TableId.Exhibitions, "Выставки", "exhibitions", A, Cw, Cr),
    Def(TableId.Halls, "Залы", "halls", A, Cw, N),
    Def(TableId.Visitors, "Посетители", "visitors", A, N, Cw),
    Def(TableId.AuthorEx, "Связь автор — экспонат", "author_ex", A, N, N),
    Def(TableId.Excursions, "Экскурсии", "excursions", A, N, Cr),
    Def(TableId.ExhibitionExhibits, "Связь выставка — экспонат", "exhibition_exhibits", A, Cw, N),
    Def(TableId.ExhibitionHalls, "Связь выставка — зал", "exhibition_halls", A, Cw, N),
    Def(TableId.ExhibitionTickets, "Билеты на выставки", "exhibition_tickets", A, N, Cw),
    Def(TableId.ExhibitMaterials, "Материалы экспонатов", "exhibit_materials", A, N, N),
    Def(TableId.ExhibitMovements, "Перемещения экспонатов", "exhibit_movements", A, N, N),
    Def(TableId.Restorations, "Реставрации", "restorations", A, N, N),
    Def(TableId.ExcursionTickets, "Билеты на экскурсии", "excursion_tickets", A, N, Cw),
    Def(TableId.Reviews, "Отзывы", "reviews", A, N, N),
    Def(TableId.Events, "Мероприятия", "events", A, N, N),
    Def(TableId.EventTickets, "Билеты на мероприятия", "event_tickets", A, N, N),
    Def(TableId.Shops, "Магазины", "shops", A, N, N),
    Def(TableId.Companies, "Компании-поставщики", "companies", A, N, N),
    Def(TableId.Products, "Товары", "products", A, N, N),
    Def(TableId.Inventory, "Остатки в магазинах", "inventory", A, N, N)
  ];

  public static IEnumerable<TableDefinition> ForRole(string role) =>
    All.Where(t => t.CanRead(role));

  public static TableDefinition? Find(TableId id) => All.FirstOrDefault(t => t.Id == id);

  public static bool IsLinkTable(TableId id) => id is
      TableId.AuthorEx
      or TableId.ExhibitionExhibits
      or TableId.ExhibitionHalls
      or TableId.ExhibitMaterials;

  private static TableDefinition Def(
    TableId id, string title, string dbName,
    TableAccessLevel admin, TableAccessLevel curator, TableAccessLevel cashier) =>
    new()
    {
      Id = id,
      Title = title,
      DbName = dbName,
      Admin = admin,
      Curator = curator,
      Cashier = cashier
    };
}
