using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Navigation;

namespace MuseumApp.Services;

public static class TableDeleteService
{
    public static bool Delete(TableId tableId, object selected)
    {
        using var context = new MuseumDbContext();

        switch (tableId)
        {
            case TableId.Adresses:
                context.Adresses.Remove((Adress)selected);
                break;
            case TableId.Countries:
                context.Countries.Remove((Country)selected);
                break;
            case TableId.Materials:
                context.Materials.Remove((Material)selected);
                break;
            case TableId.Positions:
                context.Positions.Remove((Position)selected);
                break;
            case TableId.Privileges:
                context.Privileges.Remove((Privilege)selected);
                break;
            case TableId.Reasons:
                context.Reasons.Remove((Reason)selected);
                break;
            case TableId.ExhibitConditions:
                context.ExhibitConditions.Remove((ExhibitCondition)selected);
                break;
            case TableId.Authors:
                context.Authors.Remove((Author)selected);
                break;
            case TableId.Employees:
                context.Employees.Remove((Employee)selected);
                break;
            case TableId.Museums:
                context.Museums.Remove((Museum)selected);
                break;
            case TableId.Branches:
                context.Branches.Remove((Branch)selected);
                break;
            case TableId.Storages:
                context.Storages.Remove((Storage)selected);
                break;
            case TableId.Collections:
                context.Collections.Remove((Collection)selected);
                break;
            case TableId.Exhibits:
                context.Exhibits.Remove((Exhibit)selected);
                break;
            case TableId.Exhibitions:
                context.Exhibitions.Remove((Exhibition)selected);
                break;
            case TableId.Halls:
                context.Halls.Remove((Hall)selected);
                break;
            case TableId.Visitors:
                context.Visitors.Remove((Visitor)selected);
                break;
            case TableId.AuthorEx:
                context.AuthorExLinks.Remove((AuthorExLink)selected);
                break;
            case TableId.Excursions:
                context.Excursions.Remove((Excursion)selected);
                break;
            case TableId.ExhibitionExhibits:
                context.ExhibitionExhibitLinks.Remove((ExhibitionExhibitLink)selected);
                break;
            case TableId.ExhibitionHalls:
                context.ExhibitionHallLinks.Remove((ExhibitionHallLink)selected);
                break;
            case TableId.ExhibitionTickets:
                context.ExhibitionTickets.Remove((ExhibitionTicket)selected);
                break;
            case TableId.ExhibitMaterials:
                context.ExhibitMaterialLinks.Remove((ExhibitMaterialLink)selected);
                break;
            case TableId.ExhibitMovements:
                context.ExhibitMovements.Remove((ExhibitMovement)selected);
                break;
            case TableId.Restorations:
                context.Restorations.Remove((Restoration)selected);
                break;
            case TableId.ExcursionTickets:
                context.ExcursionTickets.Remove((ExcursionTicket)selected);
                break;
            case TableId.Reviews:
                context.Reviews.Remove((Review)selected);
                break;
            case TableId.Events:
                context.Events.Remove((Event)selected);
                break;
            case TableId.EventTickets:
                context.EventTickets.Remove((EventTicket)selected);
                break;
            case TableId.Shops:
                context.Shops.Remove((Shop)selected);
                break;
            case TableId.Companies:
                context.Companies.Remove((Company)selected);
                break;
            case TableId.Products:
                context.Products.Remove((Product)selected);
                break;
            case TableId.Inventories:
                context.Inventories.Remove((Inventory)selected);
                break;
            default:
                return false;
        }

        context.SaveChanges();
        return true;
    }
}
