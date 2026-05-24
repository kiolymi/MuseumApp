using System.Windows;
using System.Windows.Controls;
using MuseumApp.Helpers;

namespace MuseumApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Title = $"ИС Музейный комплекс — Добро пожаловать, {SessionUser.Login}";

        foreach (var grid in new DataGrid[]
        {
            dg_exhibitions, dg_exhibits, dg_collections, dg_halls, dg_authors,
            dg_visitors, dg_exhibitionTickets, dg_excursionTickets, dg_excursions,
            dg_privileges, dg_employees, dg_events
        })
        {
            grid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;
        }

        foreach (var tab in new UIElement[]
        {
            tabExhibitions, tabExhibits, tabCollections, tabHalls, tabAuthors,
            tabVisitors, tabExhibitionTickets, tabExcursionTickets, tabExcursions,
            tabPrivileges, tabEmployees, tabEvents
        })
        {
            tab.Visibility = Visibility.Collapsed;
        }

        foreach (var button in new UIElement[]
        {
            btn_add_exhibitions, btn_change_exhibitions, btn_add_exhibits, btn_change_exhibits,
            btn_add_collections, btn_change_collections, btn_add_halls, btn_change_halls,
            btn_add_authors, btn_change_authors, btn_add_visitors, btn_change_visitors,
            btn_add_exhibitionTickets, btn_change_exhibitionTickets,
            btn_add_excursionTickets, btn_change_excursionTickets,
            btn_add_excursions, btn_change_excursions, btn_add_privileges, btn_change_privileges,
            btn_add_employees, btn_change_employees, btn_add_events, btn_change_events
        })
        {
            button.Visibility = Visibility.Collapsed;
        }

        if (SessionUser.Role == "admin_museum")
        {
            tabExhibitions.Visibility = Visibility.Visible;
            tabExhibits.Visibility = Visibility.Visible;
            tabCollections.Visibility = Visibility.Visible;
            tabHalls.Visibility = Visibility.Visible;
            tabAuthors.Visibility = Visibility.Visible;
            tabVisitors.Visibility = Visibility.Visible;
            tabExhibitionTickets.Visibility = Visibility.Visible;
            tabExcursionTickets.Visibility = Visibility.Visible;
            tabExcursions.Visibility = Visibility.Visible;
            tabPrivileges.Visibility = Visibility.Visible;
            tabEmployees.Visibility = Visibility.Visible;
            tabEvents.Visibility = Visibility.Visible;

            btn_add_exhibitions.Visibility = Visibility.Visible;
            btn_change_exhibitions.Visibility = Visibility.Visible;
            btn_add_exhibits.Visibility = Visibility.Visible;
            btn_change_exhibits.Visibility = Visibility.Visible;
            btn_add_collections.Visibility = Visibility.Visible;
            btn_change_collections.Visibility = Visibility.Visible;
            btn_add_halls.Visibility = Visibility.Visible;
            btn_change_halls.Visibility = Visibility.Visible;
            btn_add_authors.Visibility = Visibility.Visible;
            btn_change_authors.Visibility = Visibility.Visible;
            btn_add_visitors.Visibility = Visibility.Visible;
            btn_change_visitors.Visibility = Visibility.Visible;
            btn_add_exhibitionTickets.Visibility = Visibility.Visible;
            btn_change_exhibitionTickets.Visibility = Visibility.Visible;
            btn_add_excursionTickets.Visibility = Visibility.Visible;
            btn_change_excursionTickets.Visibility = Visibility.Visible;
            btn_add_excursions.Visibility = Visibility.Visible;
            btn_change_excursions.Visibility = Visibility.Visible;
            btn_add_privileges.Visibility = Visibility.Visible;
            btn_change_privileges.Visibility = Visibility.Visible;
            btn_add_employees.Visibility = Visibility.Visible;
            btn_change_employees.Visibility = Visibility.Visible;
            btn_add_events.Visibility = Visibility.Visible;
            btn_change_events.Visibility = Visibility.Visible;
        }
        else if (SessionUser.Role == "curator_museum")
        {
            tabExhibitions.Visibility = Visibility.Visible;
            tabExhibits.Visibility = Visibility.Visible;
            tabCollections.Visibility = Visibility.Visible;
            tabHalls.Visibility = Visibility.Visible;
            tabAuthors.Visibility = Visibility.Visible;

            btn_add_exhibitions.Visibility = Visibility.Visible;
            btn_change_exhibitions.Visibility = Visibility.Visible;
            btn_add_exhibits.Visibility = Visibility.Visible;
            btn_change_exhibits.Visibility = Visibility.Visible;
            btn_add_collections.Visibility = Visibility.Visible;
            btn_change_collections.Visibility = Visibility.Visible;
            btn_add_halls.Visibility = Visibility.Visible;
            btn_change_halls.Visibility = Visibility.Visible;
            btn_add_authors.Visibility = Visibility.Visible;
            btn_change_authors.Visibility = Visibility.Visible;
        }
        else if (SessionUser.Role == "cashier_museum")
        {
            tabExhibitions.Visibility = Visibility.Visible;
            tabExcursions.Visibility = Visibility.Visible;
            tabVisitors.Visibility = Visibility.Visible;
            tabExhibitionTickets.Visibility = Visibility.Visible;
            tabExcursionTickets.Visibility = Visibility.Visible;
            tabPrivileges.Visibility = Visibility.Visible;

            btn_add_visitors.Visibility = Visibility.Visible;
            btn_change_visitors.Visibility = Visibility.Visible;
            btn_add_exhibitionTickets.Visibility = Visibility.Visible;
            btn_change_exhibitionTickets.Visibility = Visibility.Visible;
            btn_add_excursionTickets.Visibility = Visibility.Visible;
            btn_change_excursionTickets.Visibility = Visibility.Visible;
            btn_add_privileges.Visibility = Visibility.Visible;
            btn_change_privileges.Visibility = Visibility.Visible;
        }
        else
        {
            tabExhibitions.Visibility = Visibility.Visible;
        }

        foreach (var item in mainTabs.Items)
        {
            if (item is UIElement element && element.Visibility == Visibility.Visible)
            {
                mainTabs.SelectedItem = item;
                break;
            }
        }

        Load();
    }

    private static void DataGrid_AutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        var type = Nullable.GetUnderlyingType(e.PropertyType) ?? e.PropertyType;
        if (type == typeof(string)
            || type.IsPrimitive
            || type == typeof(decimal)
            || type == typeof(DateTime)
            || type == typeof(DateOnly)
            || type == typeof(TimeOnly))
        {
            return;
        }

        e.Cancel = true;
    }
}
