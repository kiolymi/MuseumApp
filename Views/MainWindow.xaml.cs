using System.Windows;
using System.Windows.Controls;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;
using MuseumApp.Navigation;
using MuseumApp.Services;

namespace MuseumApp.Views;

public partial class MainWindow : Window
{
    private TableDefinition? _currentTable;
    private readonly List<TableTreeItem> _allTreeItems = [];

    public MainWindow()
    {
        InitializeComponent();
        txtUserHeader.Text = SessionUser.HeaderText;
        Title = $"ИС Музейный комплекс — {SessionUser.HeaderText}";

        dgData.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;

        dpTicketFrom.SelectedDate = new DateTime(2024, 1, 1);
        dpTicketTo.SelectedDate = new DateTime(2024, 12, 31);

        BuildTree();
    }

    private void BuildTree(string? filter = null)
    {
        _allTreeItems.Clear();
        foreach (var def in TableCatalog.ForRole(SessionUser.Role))
            _allTreeItems.Add(new TableTreeItem { Definition = def });

        treeTables.Items.Clear();
        var q = string.IsNullOrWhiteSpace(filter)
            ? _allTreeItems
            : _allTreeItems.Where(i =>
                i.SearchText.Contains(filter.Trim(), StringComparison.OrdinalIgnoreCase));

        foreach (var item in q)
        {
            var node = new TreeViewItem
            {
                Header = item.Definition.Title,
                Tag = item
            };
            treeTables.Items.Add(node);
        }

        if (treeTables.Items.Count > 0)
            ((TreeViewItem)treeTables.Items[0]).IsSelected = true;
        else
            ClearGrid();
    }

    private void txtSearch_TextChanged(object sender, TextChangedEventArgs e) =>
        BuildTree(txtSearch.Text);

    private void treeTables_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (treeTables.SelectedItem is not TreeViewItem { Tag: TableTreeItem item })
            return;

        _currentTable = item.Definition;
        txtTableTitle.Text = $"{item.Definition.Title}  ({item.Definition.DbName})";

        var def = item.Definition;
        var canWrite = def.CanWrite(SessionUser.Role);

        btnAdd.Visibility = canWrite ? Visibility.Visible : Visibility.Collapsed;
        btnEdit.Visibility = canWrite && !TableCatalog.IsLinkTable(def.Id)
            ? Visibility.Visible
            : Visibility.Collapsed;
        btnDelete.Visibility = def.CanDelete(SessionUser.Role)
            ? Visibility.Visible
            : Visibility.Collapsed;

        panelTicketFilter.Visibility = item.Definition.Id == TableId.ExhibitionTickets
            ? Visibility.Visible
            : Visibility.Collapsed;

        ReloadGrid();
    }

    private void ReloadGrid()
    {
        if (_currentTable == null)
            return;

        try
        {
            DateOnly? from = null;
            DateOnly? to = null;
            if (_currentTable.Id == TableId.ExhibitionTickets
                && dpTicketFrom.SelectedDate.HasValue
                && dpTicketTo.SelectedDate.HasValue)
            {
                from = DateOnly.FromDateTime(dpTicketFrom.SelectedDate.Value);
                to = DateOnly.FromDateTime(dpTicketTo.SelectedDate.Value);
            }

            dgData.ItemsSource = TableDataService.Load(_currentTable.Id, from, to);
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
        }
    }

    private void ClearGrid()
    {
        _currentTable = null;
        txtTableTitle.Text = "";
        dgData.ItemsSource = null;
        btnAdd.Visibility = Visibility.Collapsed;
        btnEdit.Visibility = Visibility.Collapsed;
        btnDelete.Visibility = Visibility.Collapsed;
        panelTicketFilter.Visibility = Visibility.Collapsed;
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        if (_currentTable == null) return;
        if (TableCrudService.TryAdd(_currentTable.Id, this))
            ReloadGrid();
    }

    private void btnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (_currentTable == null || dgData.SelectedItem == null)
        {
            MessageBox.Show("Выберите запись в таблице.");
            return;
        }

        if (TableCrudService.TryEdit(_currentTable.Id, dgData.SelectedItem, this))
            ReloadGrid();
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (_currentTable == null || dgData.SelectedItem == null)
        {
            MessageBox.Show("Выберите запись для удаления.");
            return;
        }

        var result = MessageBox.Show(
            "Удалить выбранную запись?",
            "Подтверждение",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            if (TableDeleteService.Delete(_currentTable.Id, dgData.SelectedItem))
            {
                ReloadGrid();
                MessageBox.Show("Удалено");
            }
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
        }
    }

    private void btnFilterTickets_Click(object sender, RoutedEventArgs e)
    {
        if (dpTicketFrom.SelectedDate == null || dpTicketTo.SelectedDate == null)
        {
            MessageBox.Show("Выберите даты периода.");
            return;
        }

        if (dpTicketFrom.SelectedDate > dpTicketTo.SelectedDate)
        {
            MessageBox.Show("Дата «с» не может быть позже даты «по».");
            return;
        }

        ReloadGrid();
    }

    private void btnResetTickets_Click(object sender, RoutedEventArgs e)
    {
        dpTicketFrom.SelectedDate = new DateTime(2024, 1, 1);
        dpTicketTo.SelectedDate = new DateTime(2024, 12, 31);
        ReloadGrid();
    }

    private void btnLogout_Click(object sender, RoutedEventArgs e)
    {
        SessionUser.Login = "";
        SessionUser.Password = "";
        SessionUser.Role = "";

        var login = new LoginWindow();
        login.Show();
        Close();
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
