using System.Windows;
using MuseumApp.Navigation;

namespace MuseumApp.Views;

public partial class MainWindow
{
    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        if (_currentTableId is not { } id)
            return;

        switch (id)
        {
            case TableId.Exhibitions: btn_add_exhibitions_Click(sender, e); break;
            case TableId.Exhibits: btn_add_exhibits_Click(sender, e); break;
            case TableId.Collections: btn_add_collections_Click(sender, e); break;
            case TableId.Halls: btn_add_halls_Click(sender, e); break;
            case TableId.Authors: btn_add_authors_Click(sender, e); break;
            case TableId.Visitors: btn_add_visitors_Click(sender, e); break;
            case TableId.ExhibitionTickets: btn_add_exhibitionTickets_Click(sender, e); break;
            case TableId.ExcursionTickets: btn_add_excursionTickets_Click(sender, e); break;
            case TableId.Excursions: btn_add_excursions_Click(sender, e); break;
            case TableId.Privileges: btn_add_privileges_Click(sender, e); break;
            case TableId.Employees: btn_add_employees_Click(sender, e); break;
            case TableId.Events: btn_add_events_Click(sender, e); break;
        }
    }

    private void btnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (_currentTableId is not { } id)
            return;

        switch (id)
        {
            case TableId.Exhibitions: btn_change_exhibitions_Click(sender, e); break;
            case TableId.Exhibits: btn_change_exhibits_Click(sender, e); break;
            case TableId.Collections: btn_change_collections_Click(sender, e); break;
            case TableId.Halls: btn_change_halls_Click(sender, e); break;
            case TableId.Authors: btn_change_authors_Click(sender, e); break;
            case TableId.Visitors: btn_change_visitors_Click(sender, e); break;
            case TableId.ExhibitionTickets: btn_change_exhibitionTickets_Click(sender, e); break;
            case TableId.ExcursionTickets: btn_change_excursionTickets_Click(sender, e); break;
            case TableId.Excursions: btn_change_excursions_Click(sender, e); break;
            case TableId.Privileges: btn_change_privileges_Click(sender, e); break;
            case TableId.Employees: btn_change_employees_Click(sender, e); break;
            case TableId.Events: btn_change_events_Click(sender, e); break;
        }
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (_currentTableId is not { } id)
            return;

        switch (id)
        {
            case TableId.Exhibitions: DelExhibition(); break;
            case TableId.Exhibits: DelExhibit(); break;
            case TableId.Collections: DelCollection(); break;
            case TableId.Halls: DelHall(); break;
            case TableId.Authors: DelAuthor(); break;
            case TableId.Visitors: DelVisitor(); break;
            case TableId.ExhibitionTickets: DelExhibitionTicket(); break;
            case TableId.ExcursionTickets: DelExcursionTicket(); break;
            case TableId.Excursions: DelExcursion(); break;
            case TableId.Privileges: DelPrivilege(); break;
            case TableId.Employees: DelEmployee(); break;
            case TableId.Events: DelEvent(); break;
        }
    }
}
