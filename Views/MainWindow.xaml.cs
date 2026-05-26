using System.Windows;
using System.Windows.Controls;
using MuseumApp.Helpers;
using MuseumApp.Navigation;
using MuseumApp.Services;

namespace MuseumApp.Views;

public partial class MainWindow : Window
{
    private TableId? _currentTableId;
    private TableAccessLevel _currentAccess = TableAccessLevel.Hidden;

    public MainWindow()
    {
        InitializeComponent();

        txtUser.Text = SessionUser.Login;
        txtRole.Text = RoleHelper.GetDisplayName(SessionUser.Role);
        Title = $"ИС Музейный комплекс — {txtUser.Text} ({txtRole.Text})";

        dgMain.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;

        foreach (var item in TableCatalog.GetTreeItems(SessionUser.Role))
            treeTables.Items.Add(item);

        treeTables.Loaded += (_, _) => SelectFirstTreeItem();
    }

    private void SelectFirstTreeItem()
    {
        if (treeTables.Items.Count == 0)
            return;

        treeTables.UpdateLayout();
        if (treeTables.ItemContainerGenerator.ContainerFromIndex(0) is TreeViewItem tvi)
        {
            tvi.IsSelected = true;
            tvi.BringIntoView();
        }
    }

    private void treeTables_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is TableTreeItem item)
            ShowTable(item);
    }

    private void ShowTable(TableTreeItem item)
    {
        _currentTableId = item.TableId;
        _currentAccess = item.Access;
        UpdateCrudButtons();
        LoadCurrentTable();
    }

    private void UpdateCrudButtons()
    {
        var canWrite = _currentAccess == TableAccessLevel.Full
                       && _currentTableId.HasValue
                       && TableCatalog.SupportsCrud(_currentTableId.Value);

        btnAdd.IsEnabled = canWrite;
        btnEdit.IsEnabled = canWrite;
        btnDelete.IsEnabled = canWrite;
    }

    public void LoadCurrentTable()
    {
        if (_currentTableId is not { } tableId)
            return;

        try
        {
            dgMain.ItemsSource = TableDataService.Load(tableId) as System.Collections.IEnumerable;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка загрузки: " + ex.Message);
        }
    }

    public void Load() => LoadCurrentTable();

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
