using System.Windows;
using MuseumApp.Helpers;
using MuseumApp.Navigation;
using MuseumApp.Services;
using MuseumApp.Services.Crud;

namespace MuseumApp.Views;

public partial class MainWindow
{
    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        if (_currentTableId is not { } id)
            return;

        if (TableCrudService.TryAdd(id, this))
        {
            Load();
            MessageBox.Show("Запись добавлена");
            return;
        }

        if (CrudRegistry.IsGeneric(id))
            GenericAdd();
    }

    private void btnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (_currentTableId is not { } id)
            return;

        if (dgMain.SelectedItem == null)
        {
            MessageBox.Show("Выберите запись для изменения.");
            return;
        }

        if (TableCrudService.TryEdit(id, dgMain.SelectedItem, this))
        {
            Load();
            MessageBox.Show("Запись изменена");
            return;
        }

        if (CrudRegistry.IsGeneric(id))
            GenericEdit();
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (_currentTableId is not { } id)
            return;

        if (dgMain.SelectedItem == null)
        {
            MessageBox.Show("Выберите запись для удаления.");
            return;
        }

        var result = MessageBox.Show(
            "Удалить выбранную запись?",
            "Подтверждение удаления",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            if (TableDeleteService.Delete(id, dgMain.SelectedItem))
            {
                Load();
                MessageBox.Show("Удалено");
                return;
            }

            if (CrudRegistry.IsGeneric(id))
                GenericDelete();
        }
        catch (Exception ex)
        {
            MessageBox.Show(DbExceptionHelper.GetMessage(ex), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
