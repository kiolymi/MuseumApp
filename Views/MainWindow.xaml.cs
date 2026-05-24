using System.Windows;
using System.Windows.Controls;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
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
        HookDataGridColumnGeneration();
        AttachDeleteContextMenus();
        Title = $"ИС Музейный комплекс — Добро пожаловать, {SessionUser.Login}";
        ApplyRoleVisibility();
        ConfigureActionButtons();
        Load();
    }

    private void HookDataGridColumnGeneration()
    {
        foreach (var grid in new DataGrid[]
        {
            dg_exhibitions, dg_exhibits, dg_collections, dg_halls, dg_authors,
            dg_visitors, dg_exhibitionTickets, dg_excursionTickets, dg_excursions,
            dg_privileges, dg_employees, dg_events
        })
        {
            grid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;
        }
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

    private void AttachDeleteContextMenus()
    {
        AddDeleteContextMenu(dg_exhibitions, DelExhibitionContext);
        AddDeleteContextMenu(dg_exhibits, DelExhibitContext);
        AddDeleteContextMenu(dg_collections, DelCollectionContext);
        AddDeleteContextMenu(dg_halls, DelHallContext);
        AddDeleteContextMenu(dg_authors, DelAuthorContext);
        AddDeleteContextMenu(dg_visitors, DelVisitorContext);
        AddDeleteContextMenu(dg_exhibitionTickets, DelExhibitionTicketContext);
        AddDeleteContextMenu(dg_excursionTickets, DelExcursionTicketContext);
        AddDeleteContextMenu(dg_excursions, DelExcursionContext);
        AddDeleteContextMenu(dg_privileges, DelPrivilegeContext);
        AddDeleteContextMenu(dg_employees, DelEmployeeContext);
        AddDeleteContextMenu(dg_events, DelEventContext);
    }

    private static void AddDeleteContextMenu(DataGrid grid, RoutedEventHandler handler)
    {
        var deleteItem = new MenuItem { Header = "Удалить" };
        deleteItem.Click += handler;
        grid.ContextMenu = new ContextMenu();
        grid.ContextMenu.Items.Add(deleteItem);
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

    private void ConfigureActionButtons()
    {
        HideAllActionButtons();

        switch (SessionUser.Role)
        {
            case "admin_museum":
                ShowActionButtons(
                    btn_add_exhibitions, btn_change_exhibitions, btn_add_exhibits, btn_change_exhibits,
                    btn_add_collections, btn_change_collections, btn_add_halls, btn_change_halls,
                    btn_add_authors, btn_change_authors, btn_add_visitors, btn_change_visitors,
                    btn_add_exhibitionTickets, btn_change_exhibitionTickets,
                    btn_add_excursionTickets, btn_change_excursionTickets,
                    btn_add_excursions, btn_change_excursions, btn_add_privileges, btn_change_privileges,
                    btn_add_employees, btn_change_employees, btn_add_events, btn_change_events);
                break;

            case "curator_museum":
                ShowActionButtons(
                    btn_add_exhibitions, btn_change_exhibitions, btn_add_exhibits, btn_change_exhibits,
                    btn_add_collections, btn_change_collections, btn_add_halls, btn_change_halls,
                    btn_add_authors, btn_change_authors);
                break;

            case "cashier_museum":
                ShowActionButtons(
                    btn_add_visitors, btn_change_visitors,
                    btn_add_exhibitionTickets, btn_change_exhibitionTickets,
                    btn_add_excursionTickets, btn_change_excursionTickets,
                    btn_add_privileges, btn_change_privileges);
                break;
        }
    }

    private void HideAllActionButtons()
    {
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
    }

    private static void ShowActionButtons(params UIElement[] buttons)
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

    private void btn_change_exhibitions_Click(object sender, RoutedEventArgs e)
    {
        if (dg_exhibitions.SelectedItem is not Exhibition selected) return;
        var w = new EditExhibitionWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_exhibits_Click(object sender, RoutedEventArgs e)
    {
        if (dg_exhibits.SelectedItem is not Exhibit selected) return;
        var w = new EditExhibitWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_collections_Click(object sender, RoutedEventArgs e)
    {
        if (dg_collections.SelectedItem is not Collection selected) return;
        var w = new EditCollectionWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_halls_Click(object sender, RoutedEventArgs e)
    {
        if (dg_halls.SelectedItem is not Hall selected) return;
        var w = new EditHallWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_authors_Click(object sender, RoutedEventArgs e)
    {
        if (dg_authors.SelectedItem is not Author selected) return;
        var w = new EditAuthorWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_visitors_Click(object sender, RoutedEventArgs e)
    {
        if (dg_visitors.SelectedItem is not Visitor selected) return;
        var w = new EditVisitorWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_exhibitionTickets_Click(object sender, RoutedEventArgs e)
    {
        if (dg_exhibitionTickets.SelectedItem is not ExhibitionTicket selected) return;
        var w = new EditExhibitionTicketWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_excursionTickets_Click(object sender, RoutedEventArgs e)
    {
        if (dg_excursionTickets.SelectedItem is not ExcursionTicket selected) return;
        var w = new EditExcursionTicketWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_excursions_Click(object sender, RoutedEventArgs e)
    {
        if (dg_excursions.SelectedItem is not Excursion selected) return;
        var w = new EditExcursionWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_privileges_Click(object sender, RoutedEventArgs e)
    {
        if (dg_privileges.SelectedItem is not Privilege selected) return;
        var w = new EditPrivilegeWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_employees_Click(object sender, RoutedEventArgs e)
    {
        if (dg_employees.SelectedItem is not Employee selected) return;
        var w = new EditEmployeeWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void btn_change_events_Click(object sender, RoutedEventArgs e)
    {
        if (dg_events.SelectedItem is not Event selected) return;
        var w = new EditEventWindow(selected) { Owner = this };
        if (w.ShowDialog() == true) Load();
    }

    private void DelExhibitionContext(object sender, RoutedEventArgs e)
    {
        if (dg_exhibitions.SelectedItem is Exhibition selected)
        {
            var result = MessageBox.Show(
                $"Удалить выставку {selected.ExhibitionName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.Exhibitions.Find(selected.IdExhibition);
                    if (item != null)
                    {
                        context.Exhibitions.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите выставку для удаления.");
        }
    }

    private void DelExhibitContext(object sender, RoutedEventArgs e)
    {
        if (dg_exhibits.SelectedItem is Exhibit selected)
        {
            var result = MessageBox.Show(
                $"Удалить экспонат {selected.Name}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.Exhibits.Find(selected.IdExhibit);
                    if (item != null)
                    {
                        context.Exhibits.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите экспонат для удаления.");
        }
    }

    private void DelCollectionContext(object sender, RoutedEventArgs e)
    {
        if (dg_collections.SelectedItem is Collection selected)
        {
            var result = MessageBox.Show(
                $"Удалить коллекцию {selected.CollectionName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.Collections.Find(selected.IdCollection);
                    if (item != null)
                    {
                        context.Collections.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите коллекцию для удаления.");
        }
    }

    private void DelHallContext(object sender, RoutedEventArgs e)
    {
        if (dg_halls.SelectedItem is Hall selected)
        {
            var result = MessageBox.Show(
                $"Удалить зал {selected.HallName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.Halls.Find(selected.IdHall);
                    if (item != null)
                    {
                        context.Halls.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите зал для удаления.");
        }
    }

    private void DelAuthorContext(object sender, RoutedEventArgs e)
    {
        if (dg_authors.SelectedItem is Author selected)
        {
            var result = MessageBox.Show(
                $"Удалить автора {selected.LastName} {selected.FirstName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.Authors.Find(selected.IdAuthor);
                    if (item != null)
                    {
                        context.Authors.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите автора для удаления.");
        }
    }

    private void DelVisitorContext(object sender, RoutedEventArgs e)
    {
        if (dg_visitors.SelectedItem is Visitor selected)
        {
            var result = MessageBox.Show(
                $"Удалить посетителя {selected.LastName} {selected.FirstName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.Visitors.Find(selected.IdVisitor);
                    if (item != null)
                    {
                        context.Visitors.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите посетителя для удаления.");
        }
    }

    private void DelExhibitionTicketContext(object sender, RoutedEventArgs e)
    {
        if (dg_exhibitionTickets.SelectedItem is ExhibitionTicket selected)
        {
            var result = MessageBox.Show(
                $"Удалить билет (посетитель {selected.IdVisitor}, выставка {selected.IdExhibition})?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.ExhibitionTickets.Find(selected.IdExhibition, selected.IdVisitor);
                    if (item != null)
                    {
                        context.ExhibitionTickets.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите билет для удаления.");
        }
    }

    private void DelExcursionTicketContext(object sender, RoutedEventArgs e)
    {
        if (dg_excursionTickets.SelectedItem is ExcursionTicket selected)
        {
            var result = MessageBox.Show(
                $"Удалить билет (посетитель {selected.IdVisitor}, экскурсия {selected.IdExcursion})?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.ExcursionTickets.Find(selected.IdVisitor, selected.IdExcursion);
                    if (item != null)
                    {
                        context.ExcursionTickets.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите билет для удаления.");
        }
    }

    private void DelExcursionContext(object sender, RoutedEventArgs e)
    {
        if (dg_excursions.SelectedItem is Excursion selected)
        {
            var result = MessageBox.Show(
                $"Удалить экскурсию #{selected.IdExcursion}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.Excursions.Find(selected.IdExcursion);
                    if (item != null)
                    {
                        context.Excursions.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите экскурсию для удаления.");
        }
    }

    private void DelPrivilegeContext(object sender, RoutedEventArgs e)
    {
        if (dg_privileges.SelectedItem is Privilege selected)
        {
            var result = MessageBox.Show(
                $"Удалить льготу {selected.PrivilegeName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.Privileges.Find(selected.IdPrivilege);
                    if (item != null)
                    {
                        context.Privileges.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите льготу для удаления.");
        }
    }

    private void DelEmployeeContext(object sender, RoutedEventArgs e)
    {
        if (dg_employees.SelectedItem is Employee selected)
        {
            var result = MessageBox.Show(
                $"Удалить сотрудника {selected.LastName} {selected.FirstName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.Employees.Find(selected.IdEmployee);
                    if (item != null)
                    {
                        context.Employees.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите сотрудника для удаления.");
        }
    }

    private void DelEventContext(object sender, RoutedEventArgs e)
    {
        if (dg_events.SelectedItem is Event selected)
        {
            var result = MessageBox.Show(
                $"Удалить мероприятие {selected.EventName}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var context = new MuseumDbContext();
                    var item = context.Events.Find(selected.IdEvent);
                    if (item != null)
                    {
                        context.Events.Remove(item);
                        context.SaveChanges();
                        Load();
                        MessageBox.Show("Удалено");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Выберите мероприятие для удаления.");
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
