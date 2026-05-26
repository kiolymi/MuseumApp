using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Navigation;
using MuseumApp.Queries;

namespace MuseumApp.Services;

public static class TableDataService
{
    public static IEnumerable<object> Load(TableId tableId, DateOnly? ticketFrom = null, DateOnly? ticketTo = null)
    {
        using var context = new MuseumDbContext();
        return tableId switch
        {
            TableId.Adresses => context.Adresses.OrderBy(x => x.IdAddress).Cast<object>().ToList(),
            TableId.Countries => context.Countries.OrderBy(x => x.IdCountry).Cast<object>().ToList(),
            TableId.Materials => context.Materials.OrderBy(x => x.IdMaterial).Cast<object>().ToList(),
            TableId.Positions => context.Positions.OrderBy(x => x.IdPosition).Cast<object>().ToList(),
            TableId.Privileges => context.Privileges.OrderBy(x => x.IdPrivilege).Cast<object>().ToList(),
            TableId.Reasons => context.Reasons.OrderBy(x => x.IdReason).Cast<object>().ToList(),
            TableId.ExhibitConditions => context.ExhibitConditions.OrderBy(x => x.IdCondition).Cast<object>().ToList(),
            TableId.Authors => context.Authors.OrderBy(x => x.IdAuthor).Cast<object>().ToList(),
            TableId.Employees => context.Employees.OrderBy(x => x.IdEmployee).Cast<object>().ToList(),
            TableId.Museum => context.Museums.OrderBy(x => x.IdMuseum).Cast<object>().ToList(),
            TableId.Branches => context.Branches.OrderBy(x => x.IdBranch).Cast<object>().ToList(),
            TableId.Storages => context.Storages.OrderBy(x => x.IdStorage).Cast<object>().ToList(),
            TableId.Collections => context.Collections.OrderBy(x => x.IdCollection).Cast<object>().ToList(),
            TableId.Exhibits => context.Exhibits.OrderBy(x => x.IdExhibit).Cast<object>().ToList(),
            TableId.Exhibitions => context.Exhibitions.OrderBy(x => x.IdExhibition).Cast<object>().ToList(),
            TableId.Halls => context.Halls.OrderBy(x => x.IdHall).Cast<object>().ToList(),
            TableId.Visitors => context.Visitors.OrderBy(x => x.IdVisitor).Cast<object>().ToList(),
            TableId.AuthorEx => context.AuthorExLinks.OrderBy(x => x.IdAuthor).Cast<object>().ToList(),
            TableId.Excursions => context.Excursions.OrderBy(x => x.IdExcursion).Cast<object>().ToList(),
            TableId.ExhibitionExhibits => context.ExhibitionExhibitLinks.OrderBy(x => x.IdExhibition).Cast<object>().ToList(),
            TableId.ExhibitionHalls => context.ExhibitionHallLinks.OrderBy(x => x.IdExhibition).Cast<object>().ToList(),
            TableId.ExhibitionTickets => LoadExhibitionTickets(ticketFrom, ticketTo),
            TableId.ExhibitMaterials => context.ExhibitMaterialLinks.OrderBy(x => x.IdExhibit).Cast<object>().ToList(),
            TableId.ExhibitMovements => context.ExhibitMovements.OrderBy(x => x.IdMovement).Cast<object>().ToList(),
            TableId.Restorations => context.Restorations.OrderBy(x => x.IdRestoration).Cast<object>().ToList(),
            TableId.ExcursionTickets => context.ExcursionTickets.OrderBy(x => x.IdVisitor).Cast<object>().ToList(),
            TableId.Reviews => context.Reviews.OrderBy(x => x.IdReview).Cast<object>().ToList(),
            TableId.Events => context.Events.OrderBy(x => x.IdEvent).Cast<object>().ToList(),
            TableId.EventTickets => context.EventTickets.OrderBy(x => x.IdVisitor).Cast<object>().ToList(),
            TableId.Shops => context.Shops.OrderBy(x => x.IdShop).Cast<object>().ToList(),
            TableId.Companies => context.Companies.OrderBy(x => x.IdCompany).Cast<object>().ToList(),
            TableId.Products => context.Products.OrderBy(x => x.IdProduct).Cast<object>().ToList(),
            TableId.Inventory => context.Inventories.OrderBy(x => x.IdShop).Cast<object>().ToList(),
            _ => Array.Empty<object>()
        };
    }

    private static List<object> LoadExhibitionTickets(DateOnly? from, DateOnly? to)
    {
        if (from.HasValue && to.HasValue)
            return new MuseumQueries().TicketsByPeriod(from.Value, to.Value).Cast<object>().ToList();

        using var context = new MuseumDbContext();
        return context.ExhibitionTickets.OrderBy(t => t.VisitDate).Cast<object>().ToList();
    }
}
