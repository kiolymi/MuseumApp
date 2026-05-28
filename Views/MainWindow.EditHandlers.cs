using System.Windows;
using MuseumApp.Data.Entities;
using MuseumApp.Windows.Authors;
using MuseumApp.Windows.Collections;
using MuseumApp.Windows.Employees;
using MuseumApp.Windows.Events;
using MuseumApp.Windows.Excursions;
using MuseumApp.Windows.Exhibitions;
using MuseumApp.Windows.Exhibits;
using MuseumApp.Windows.Halls;
using MuseumApp.Windows.Privileges;
using MuseumApp.Windows.Tickets;
using MuseumApp.Windows.Visitors;

namespace MuseumApp.Views;

public partial class MainWindow
{
    private void btn_change_exhibitions_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as Exhibition;
        if (selected == null) return;
        var w = new EditExhibitionWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_exhibits_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as Exhibit;
        if (selected == null) return;
        var w = new EditExhibitWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_collections_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as Collection;
        if (selected == null) return;
        var w = new EditCollectionWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_halls_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as Hall;
        if (selected == null) return;
        var w = new EditHallWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_authors_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as Author;
        if (selected == null) return;
        var w = new EditAuthorWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_visitors_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as Visitor;
        if (selected == null) return;
        var w = new EditVisitorWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_exhibitionTickets_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as ExhibitionTicket;
        if (selected == null) return;
        var w = new EditExhibitionTicketWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_excursionTickets_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as ExcursionTicket;
        if (selected == null) return;
        var w = new EditExcursionTicketWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_excursions_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as Excursion;
        if (selected == null) return;
        var w = new EditExcursionWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_privileges_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as Privilege;
        if (selected == null) return;
        var w = new EditPrivilegeWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_employees_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as Employee;
        if (selected == null) return;
        var w = new EditEmployeeWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_events_Click(object sender, RoutedEventArgs e)
    {
        var selected = dgMain.SelectedItem as Event;
        if (selected == null) return;
        var w = new EditEventWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }
}
