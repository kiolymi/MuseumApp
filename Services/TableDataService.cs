using MuseumApp.Data;
using MuseumApp.Navigation;

namespace MuseumApp.Services;

public static class TableDataService
{
    public static object? Load(TableId tableId)
    {
        using var context = new MuseumDbContext();
        return tableId switch
        {
            TableId.Adresses => context.Adresses.ToList(),
            TableId.Authors => context.Authors.ToList(),
            TableId.Branches => context.Branches.ToList(),
            TableId.Collections => context.Collections.ToList(),
            TableId.Companies => context.Companies.ToList(),
            TableId.Countries => context.Countries.ToList(),
            TableId.Employees => context.Employees.ToList(),
            TableId.Events => context.Events.ToList(),
            TableId.EventTickets => context.EventTickets.ToList(),
            TableId.Excursions => context.Excursions.ToList(),
            TableId.ExcursionTickets => context.ExcursionTickets.ToList(),
            TableId.Exhibits => context.Exhibits.ToList(),
            TableId.ExhibitConditions => context.ExhibitConditions.ToList(),
            TableId.ExhibitMovements => context.ExhibitMovements.ToList(),
            TableId.Exhibitions => context.Exhibitions.ToList(),
            TableId.ExhibitionTickets => context.ExhibitionTickets.ToList(),
            TableId.Halls => context.Halls.ToList(),
            TableId.Inventories => context.Inventories.ToList(),
            TableId.Materials => context.Materials.ToList(),
            TableId.Museums => context.Museums.ToList(),
            TableId.Positions => context.Positions.ToList(),
            TableId.Privileges => context.Privileges.ToList(),
            TableId.Products => context.Products.ToList(),
            TableId.Reasons => context.Reasons.ToList(),
            TableId.Restorations => context.Restorations.ToList(),
            TableId.Reviews => context.Reviews.ToList(),
            TableId.Shops => context.Shops.ToList(),
            TableId.Storages => context.Storages.ToList(),
            TableId.Visitors => context.Visitors.ToList(),
            TableId.AuthorEx => context.AuthorExLinks.ToList(),
            TableId.ExhibitionExhibits => context.ExhibitionExhibitLinks.ToList(),
            TableId.ExhibitionHalls => context.ExhibitionHallLinks.ToList(),
            TableId.ExhibitMaterials => context.ExhibitMaterialLinks.ToList(),
            TableId.VwActiveExhibitions => context.VwActiveExhibitions.ToList(),
            TableId.VwEmployeeDuties => context.VwEmployeeDuties.ToList(),
            TableId.VwExhibitFullInfos => context.VwExhibitFullInfos.ToList(),
            TableId.VwProductStocks => context.VwProductStocks.ToList(),
            TableId.VwStorageOccupancies => context.VwStorageOccupancies.ToList(),
            TableId.VwVisitorHistories => context.VwVisitorHistories.ToList(),
            _ => null
        };
    }
}
