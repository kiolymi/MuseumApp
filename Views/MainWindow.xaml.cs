using System.Windows;
using MuseumApp.Data;
using MuseumApp.Helpers;
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

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Title = $"ИС Музейный комплекс — Добро пожаловать, {SessionUser.Login}";
        ApplyRoleVisibility();
        ConfigureAddButtons();
        Load();
    }

    public void Load()
    {
        try
        {
            var context = new MuseumDbContext();

            switch (SessionUser.Role)
            {
                case "admin_museum":
                    dg_exhibitions.ItemsSource = context.Exhibitions.ToList();
                    dg_exhibits.ItemsSource = context.Exhibits.ToList();
                    dg_collections.ItemsSource = context.Collections.ToList();
                    dg_halls.ItemsSource = context.Halls.ToList();
                    dg_authors.ItemsSource = context.Authors.ToList();
                    dg_visitors.ItemsSource = context.Visitors.ToList();
                    dg_exhibitionTickets.ItemsSource = context.ExhibitionTickets.ToList();
                    dg_excursionTickets.ItemsSource = context.ExcursionTickets.ToList();
                    dg_excursions.ItemsSource = context.Excursions.ToList();
                    dg_privileges.ItemsSource = context.Privileges.ToList();
                    dg_employees.ItemsSource = context.Employees.ToList();
                    dg_events.ItemsSource = context.Events.ToList();
                    break;

                case "curator_museum":
                    dg_exhibitions.ItemsSource = context.Exhibitions.ToList();
                    dg_exhibits.ItemsSource = context.Exhibits.ToList();
                    dg_collections.ItemsSource = context.Collections.ToList();
                    dg_halls.ItemsSource = context.Halls.ToList();
                    dg_authors.ItemsSource = context.Authors.ToList();
                    break;

                case "cashier_museum":
                    dg_exhibitions.ItemsSource = context.Exhibitions.ToList();
                    dg_excursions.ItemsSource = context.Excursions.ToList();
                    dg_visitors.ItemsSource = context.Visitors.ToList();
                    dg_exhibitionTickets.ItemsSource = context.ExhibitionTickets.ToList();
                    dg_excursionTickets.ItemsSource = context.ExcursionTickets.ToList();
                    dg_privileges.ItemsSource = context.Privileges.ToList();
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка загрузки: " + ex.Message);
        }
    }

    private void ConfigureAddButtons()
    {
        HideAllAddButtons();

        switch (SessionUser.Role)
        {
            case "admin_museum":
                ShowAddButtons(
                    btn_add_exhibitions, btn_add_exhibits, btn_add_collections, btn_add_halls, btn_add_authors,
                    btn_add_visitors, btn_add_exhibitionTickets, btn_add_excursionTickets, btn_add_excursions,
                    btn_add_privileges, btn_add_employees, btn_add_events);
                break;

            case "curator_museum":
                ShowAddButtons(
                    btn_add_exhibitions, btn_add_exhibits, btn_add_collections, btn_add_halls, btn_add_authors);
                break;

            case "cashier_museum":
                ShowAddButtons(
                    btn_add_visitors, btn_add_exhibitionTickets, btn_add_excursionTickets, btn_add_privileges);
                break;
        }
    }

    private void HideAllAddButtons()
    {
        foreach (var button in new[]
        {
            btn_add_exhibitions, btn_add_exhibits, btn_add_collections, btn_add_halls, btn_add_authors,
            btn_add_visitors, btn_add_exhibitionTickets, btn_add_excursionTickets, btn_add_excursions,
            btn_add_privileges, btn_add_employees, btn_add_events
        })
        {
            button.Visibility = Visibility.Collapsed;
        }
    }

    private static void ShowAddButtons(params UIElement[] buttons)
    {
        foreach (var button in buttons)
        {
            button.Visibility = Visibility.Visible;
        }
    }

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

    private void ApplyRoleVisibility()
    {
        HideAllTabs();

        switch (SessionUser.Role)
        {
            case "admin_museum":
                ShowTabs(
                    tabExhibitions, tabExhibits, tabCollections, tabHalls, tabAuthors,
                    tabVisitors, tabExhibitionTickets, tabExcursionTickets, tabExcursions,
                    tabPrivileges, tabEmployees, tabEvents);
                break;

            case "curator_museum":
                ShowTabs(tabExhibitions, tabExhibits, tabCollections, tabHalls, tabAuthors);
                break;

            case "cashier_museum":
                ShowTabs(
                    tabExhibitions, tabExcursions,
                    tabVisitors, tabExhibitionTickets, tabExcursionTickets, tabPrivileges);
                break;

            default:
                tabExhibitions.Visibility = Visibility.Visible;
                break;
        }

        SelectFirstVisibleTab();
    }

    private void HideAllTabs()
    {
        foreach (var tab in new[]
        {
            tabExhibitions, tabExhibits, tabCollections, tabHalls, tabAuthors,
            tabVisitors, tabExhibitionTickets, tabExcursionTickets, tabExcursions,
            tabPrivileges, tabEmployees, tabEvents
        })
        {
            tab.Visibility = Visibility.Collapsed;
        }
    }

    private static void ShowTabs(params UIElement[] tabs)
    {
        foreach (var tab in tabs)
        {
            tab.Visibility = Visibility.Visible;
        }
    }

    private void SelectFirstVisibleTab()
    {
        foreach (var item in mainTabs.Items)
        {
            if (item is UIElement element && element.Visibility == Visibility.Visible)
            {
                mainTabs.SelectedItem = item;
                return;
            }
        }
    }
}
