using System.Windows;
using MuseumApp.Data.Entities;
using MuseumApp.Navigation;
using MuseumApp.Windows.Authors;
using MuseumApp.Windows.Collections;
using MuseumApp.Windows.Countries;
using MuseumApp.Windows.Employees;
using MuseumApp.Windows.Events;
using MuseumApp.Windows.Excursions;
using MuseumApp.Windows.Exhibitions;
using MuseumApp.Windows.Exhibits;
using MuseumApp.Windows.Halls;
using MuseumApp.Windows.Links;
using MuseumApp.Windows.Privileges;
using MuseumApp.Windows.Reasons;
using MuseumApp.Windows.Tickets;
using MuseumApp.Windows.Visitors;
using MuseumApp.Windows.Adresses;
using MuseumApp.Windows.Materials;
using MuseumApp.Windows.Positions;
using MuseumApp.Windows.ExhibitConditions;
using MuseumApp.Windows.Museums;
using MuseumApp.Windows.Branches;
using MuseumApp.Windows.Storages;
using MuseumApp.Windows.ExhibitMovements;
using MuseumApp.Windows.Restorations;
using MuseumApp.Windows.Reviews;
using MuseumApp.Windows.EventTickets;
using MuseumApp.Windows.Shops;
using MuseumApp.Windows.Companies;
using MuseumApp.Windows.Products;
using MuseumApp.Windows.Inventories;

namespace MuseumApp.Services;

public static class TableCrudService
{
    private static readonly TableId[] LinkTables =
    [
        TableId.AuthorEx,
        TableId.ExhibitionExhibits,
        TableId.ExhibitionHalls,
        TableId.ExhibitMaterials
    ];

    public static bool TryAdd(TableId tableId, Window owner)
    {
        Window? dialog = tableId switch
        {
            TableId.Adresses => new AddAdressWindow(),
            TableId.Countries => new AddCountryWindow(),
            TableId.Materials => new AddMaterialWindow(),
            TableId.Positions => new AddPositionWindow(),
            TableId.Privileges => new AddPrivilegeWindow(),
            TableId.Reasons => new AddReasonWindow(),
            TableId.ExhibitConditions => new AddExhibitConditionWindow(),
            TableId.Authors => new AddAuthorWindow(),
            TableId.Employees => new AddEmployeeWindow(),
            TableId.Museums => new AddMuseumWindow(),
            TableId.Branches => new AddBranchWindow(),
            TableId.Storages => new AddStorageWindow(),
            TableId.Collections => new AddCollectionWindow(),
            TableId.Exhibits => new AddExhibitWindow(),
            TableId.Exhibitions => new AddExhibitionWindow(),
            TableId.Halls => new AddHallWindow(),
            TableId.Visitors => new AddVisitorWindow(),
            TableId.AuthorEx => new AddAuthorExLinkWindow(),
            TableId.Excursions => new AddExcursionWindow(),
            TableId.ExhibitionExhibits => new AddExhibitionExhibitLinkWindow(),
            TableId.ExhibitionHalls => new AddExhibitionHallLinkWindow(),
            TableId.ExhibitionTickets => new AddExhibitionTicketWindow(),
            TableId.ExhibitMaterials => new AddExhibitMaterialLinkWindow(),
            TableId.ExhibitMovements => new AddExhibitMovementWindow(),
            TableId.Restorations => new AddRestorationWindow(),
            TableId.ExcursionTickets => new AddExcursionTicketWindow(),
            TableId.Reviews => new AddReviewWindow(),
            TableId.Events => new AddEventWindow(),
            TableId.EventTickets => new AddEventTicketWindow(),
            TableId.Shops => new AddShopWindow(),
            TableId.Companies => new AddCompanyWindow(),
            TableId.Products => new AddProductWindow(),
            TableId.Inventories => new AddInventoryWindow(),
            _ => null
        };

        return ShowDialog(dialog, owner);
    }

    public static bool TryEdit(TableId tableId, object selected, Window owner)
    {
        if (LinkTables.Contains(tableId))
        {
            MessageBox.Show(owner, "Для связей многие-ко-многим удалите запись и добавьте новую.");
            return false;
        }

        Window? dialog = tableId switch
        {
            TableId.Adresses => new EditAdressWindow((Adress)selected),
            TableId.Countries => new EditCountryWindow((Country)selected),
            TableId.Materials => new EditMaterialWindow((Material)selected),
            TableId.Positions => new EditPositionWindow((Position)selected),
            TableId.Privileges => new EditPrivilegeWindow((Privilege)selected),
            TableId.Reasons => new EditReasonWindow((Reason)selected),
            TableId.ExhibitConditions => new EditExhibitConditionWindow((ExhibitCondition)selected),
            TableId.Authors => new EditAuthorWindow((Author)selected),
            TableId.Employees => new EditEmployeeWindow((Employee)selected),
            TableId.Museums => new EditMuseumWindow((Museum)selected),
            TableId.Branches => new EditBranchWindow((Branch)selected),
            TableId.Storages => new EditStorageWindow((Storage)selected),
            TableId.Collections => new EditCollectionWindow((Collection)selected),
            TableId.Exhibits => new EditExhibitWindow((Exhibit)selected),
            TableId.Exhibitions => new EditExhibitionWindow((Exhibition)selected),
            TableId.Halls => new EditHallWindow((Hall)selected),
            TableId.Visitors => new EditVisitorWindow((Visitor)selected),
            TableId.Excursions => new EditExcursionWindow((Excursion)selected),
            TableId.ExhibitionTickets => new EditExhibitionTicketWindow((ExhibitionTicket)selected),
            TableId.ExhibitMovements => new EditExhibitMovementWindow((ExhibitMovement)selected),
            TableId.Restorations => new EditRestorationWindow((Restoration)selected),
            TableId.ExcursionTickets => new EditExcursionTicketWindow((ExcursionTicket)selected),
            TableId.Reviews => new EditReviewWindow((Review)selected),
            TableId.Events => new EditEventWindow((Event)selected),
            TableId.EventTickets => new EditEventTicketWindow((EventTicket)selected),
            TableId.Shops => new EditShopWindow((Shop)selected),
            TableId.Companies => new EditCompanyWindow((Company)selected),
            TableId.Products => new EditProductWindow((Product)selected),
            TableId.Inventories => new EditInventoryWindow((Inventory)selected),
            _ => null
        };

        return ShowDialog(dialog, owner);
    }

    private static bool ShowDialog(Window? dialog, Window owner)
    {
        if (dialog == null)
            return false;

        dialog.Owner = owner;
        return dialog.ShowDialog() == true;
    }
}
