namespace MuseumApp.Navigation;

public static class TableCatalog
{
    private static readonly TableDefinition[] All =
    [
        Def(TableId.Adresses, "Адреса", AdminFull),
        Def(TableId.Authors, "Авторы", CuratorFull),
        Def(TableId.Branches, "Филиалы", AdminFull),
        Def(TableId.Collections, "Коллекции", CuratorFull),
        Def(TableId.Companies, "Компании", AdminFull),
        Def(TableId.Countries, "Страны", AdminFull),
        Def(TableId.Employees, "Сотрудники", AdminFull),
        Def(TableId.Events, "Мероприятия", AdminFull),
        Def(TableId.EventTickets, "Билеты на мероприятия", AdminFull),
        Def(TableId.Excursions, "Экскурсии", CashierExcursionFull),
        Def(TableId.ExcursionTickets, "Билеты на экскурсии", CashierTicketFull),
        Def(TableId.Exhibits, "Экспонаты", CuratorFull),
        Def(TableId.ExhibitConditions, "Состояния экспонатов", AdminFull),
        Def(TableId.ExhibitMovements, "Перемещения экспонатов", AdminFull),
        Def(TableId.Exhibitions, "Выставки", CuratorExhibitionAccess),
        Def(TableId.ExhibitionTickets, "Билеты на выставки", CashierTicketFull),
        Def(TableId.Halls, "Залы", CuratorFull),
        Def(TableId.Inventories, "Инвентаризация", AdminFull),
        Def(TableId.Materials, "Материалы", AdminFull),
        Def(TableId.Museums, "Музеи", AdminFull),
        Def(TableId.Positions, "Должности", AdminFull),
        Def(TableId.Privileges, "Льготы", CashierPrivilegeFull),
        Def(TableId.Products, "Товары", AdminFull),
        Def(TableId.Reasons, "Причины", AdminFull),
        Def(TableId.Restorations, "Реставрации", AdminFull),
        Def(TableId.Reviews, "Отзывы", AdminFull),
        Def(TableId.Shops, "Магазины", AdminFull),
        Def(TableId.Storages, "Хранилища", AdminFull),
        Def(TableId.Visitors, "Посетители", CashierVisitorFull),
        Def(TableId.VwActiveExhibitions, "Представление: активные выставки", AdminRead),
        Def(TableId.VwEmployeeDuties, "Представление: дежурства сотрудников", AdminRead),
        Def(TableId.VwExhibitFullInfos, "Представление: экспонаты (полная информация)", AdminRead),
        Def(TableId.VwProductStocks, "Представление: остатки товаров", AdminRead),
        Def(TableId.VwStorageOccupancies, "Представление: заполненность хранилищ", AdminRead),
        Def(TableId.VwVisitorHistories, "Представление: история посетителей", AdminRead),
    ];

    public static IReadOnlyList<TableTreeItem> GetTreeItems(string role) =>
        All.Select(d => new TableTreeItem
        {
            Title = d.Title,
            TableId = d.Id,
            Access = d.GetAccess(role)
        })
        .Where(i => i.Access != TableAccessLevel.Hidden)
        .OrderBy(i => i.Title, StringComparer.CurrentCultureIgnoreCase)
        .ToList();

    public static TableAccessLevel GetAccess(TableId id, string role) =>
        All.FirstOrDefault(d => d.Id == id)?.GetAccess(role) ?? TableAccessLevel.Hidden;

    public static bool SupportsCrud(TableId id) => id switch
    {
        TableId.Exhibitions or TableId.Exhibits or TableId.Collections or TableId.Halls or TableId.Authors
            or TableId.Visitors or TableId.ExhibitionTickets or TableId.ExcursionTickets or TableId.Excursions
            or TableId.Privileges or TableId.Employees or TableId.Events => true,
        _ => false
    };

    private static TableDefinition Def(TableId id, string title, Func<string, TableAccessLevel> getAccess) =>
        new() { Id = id, Title = title, GetAccess = getAccess };

    private static TableAccessLevel AdminFull(string role) =>
        role == "admin_museum" ? TableAccessLevel.Full : TableAccessLevel.Hidden;

    private static TableAccessLevel AdminRead(string role) =>
        role == "admin_museum" ? TableAccessLevel.ReadOnly : TableAccessLevel.Hidden;

    private static TableAccessLevel CuratorFull(string role) => role switch
    {
        "admin_museum" => TableAccessLevel.Full,
        "curator_museum" => TableAccessLevel.Full,
        _ => TableAccessLevel.Hidden
    };

    private static TableAccessLevel CuratorExhibitionAccess(string role) => role switch
    {
        "admin_museum" => TableAccessLevel.Full,
        "curator_museum" => TableAccessLevel.Full,
        "cashier_museum" => TableAccessLevel.ReadOnly,
        _ => TableAccessLevel.Hidden
    };

    private static TableAccessLevel CashierTicketFull(string role) => role switch
    {
        "admin_museum" => TableAccessLevel.Full,
        "cashier_museum" => TableAccessLevel.Full,
        _ => TableAccessLevel.Hidden
    };

    private static TableAccessLevel CashierExcursionFull(string role) => role switch
    {
        "admin_museum" => TableAccessLevel.Full,
        "cashier_museum" => TableAccessLevel.ReadOnly,
        _ => TableAccessLevel.Hidden
    };

    private static TableAccessLevel CashierVisitorFull(string role) => role switch
    {
        "admin_museum" => TableAccessLevel.Full,
        "cashier_museum" => TableAccessLevel.Full,
        _ => TableAccessLevel.Hidden
    };

    private static TableAccessLevel CashierPrivilegeFull(string role) => role switch
    {
        "admin_museum" => TableAccessLevel.Full,
        "cashier_museum" => TableAccessLevel.Full,
        _ => TableAccessLevel.Hidden
    };
}
