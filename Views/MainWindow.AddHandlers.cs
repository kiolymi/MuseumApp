using System.Windows;
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
    private void btn_add_exhibitions_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddExhibitionWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Выставка добавлена");
        }
    }

    private void btn_add_exhibits_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddExhibitWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Экспонат добавлен");
        }
    }

    private void btn_add_collections_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddCollectionWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Коллекция добавлена");
        }
    }

    private void btn_add_halls_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddHallWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Зал добавлен");
        }
    }

    private void btn_add_authors_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddAuthorWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Автор добавлен");
        }
    }

    private void btn_add_visitors_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddVisitorWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Посетитель добавлен");
        }
    }

    private void btn_add_exhibitionTickets_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddExhibitionTicketWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Билет на выставку добавлен");
        }
    }

    private void btn_add_excursionTickets_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddExcursionTicketWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Билет на экскурсию добавлен");
        }
    }

    private void btn_add_excursions_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddExcursionWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Экскурсия добавлена");
        }
    }

    private void btn_add_privileges_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddPrivilegeWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Льгота добавлена");
        }
    }

    private void btn_add_employees_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddEmployeeWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Сотрудник добавлен");
        }
    }

    private void btn_add_events_Click(object sender, RoutedEventArgs e)
    {
        var w = new AddEventWindow { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Мероприятие добавлено");
        }
    }
}
