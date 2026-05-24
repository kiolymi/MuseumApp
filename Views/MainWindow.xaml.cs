using System.Windows;
using MuseumApp.Helpers;

namespace MuseumApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Title = $"ИС Музейный комплекс — Добро пожаловать, {SessionUser.Login}";
        ApplyRoleVisibility();
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
