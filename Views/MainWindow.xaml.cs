using System.Windows;
using System.Windows.Controls;
using MuseumApp.Data.Entities;
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
        Title = "ИС Музейный комплекс — " + txtUser.Text + " (" + txtRole.Text + ")";

        dgMain.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;

        foreach (var item in TableCatalog.GetTreeItems(SessionUser.Role))
        {
            treeTables.Items.Add(item);
        }

        treeTables.Loaded += TreeTables_OnLoaded;
    }

    private void TreeTables_OnLoaded(object sender, RoutedEventArgs e)
    {
        SelectFirstTreeItem();
    }

    private void SelectFirstTreeItem()
    {
        if (treeTables.Items.Count == 0)
        {
            return;
        }

        treeTables.UpdateLayout();

        if (treeTables.ItemContainerGenerator.ContainerFromIndex(0) is TreeViewItem firstItem)
        {
            firstItem.IsSelected = true;
            firstItem.BringIntoView();
        }
    }

    private void treeTables_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is TableTreeItem item)
        {
            ShowTable(item);
        }
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
        var canWrite = false;

        if (_currentAccess == TableAccessLevel.Full && _currentTableId.HasValue)
        {
            canWrite = TableCatalog.SupportsCrud(_currentTableId.Value);
        }

        Visibility visibility;
        if (canWrite)
        {
            visibility = Visibility.Visible;
        }
        else
        {
            visibility = Visibility.Collapsed;
        }

        btnAdd.Visibility = visibility;
        btnEdit.Visibility = visibility;
        btnDelete.Visibility = visibility;
    }

    public void LoadCurrentTable()
    {
        if (!_currentTableId.HasValue)
        {
            return;
        }

        var tableId = _currentTableId.Value;

        try
        {
            dgMain.ItemsSource = TableDataService.Load(tableId) as System.Collections.IEnumerable;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка загрузки: " + ex.Message);
        }
    }

    public void Load()
    {
        LoadCurrentTable();
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

        if (type == typeof(string))
        {
            return;
        }

        if (type.IsPrimitive)
        {
            return;
        }

        if (type == typeof(decimal))
        {
            return;
        }

        if (type == typeof(DateTime))
        {
            return;
        }

        if (type == typeof(DateOnly))
        {
            return;
        }

        if (type == typeof(TimeOnly))
        {
            return;
        }

        e.Cancel = true;
    }
}
