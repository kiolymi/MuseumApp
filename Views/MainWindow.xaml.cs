using System.Windows;
using MuseumApp.Data;
using MuseumApp.Helpers;

namespace MuseumApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Title = $"ИС Музейный комплекс — Добро пожаловать, {SessionUser.Login}";
        ApplyRoleVisibility();
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
