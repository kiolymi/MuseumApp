using System.Windows;
using MuseumApp.Navigation;
using MuseumApp.Services.Crud;
using MuseumApp.Windows.Generic;

namespace MuseumApp.Views;

public partial class MainWindow
{
    private void GenericAdd()
    {
        if (_currentTableId is not { } id || !CrudRegistry.TryGet(id, out var spec))
            return;

        var w = new GenericRecordWindow(id) { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Запись добавлена");
        }
    }

    private void GenericEdit()
    {
        if (_currentTableId is not { } id || !CrudRegistry.TryGet(id, out var spec))
            return;

        if (dgMain.SelectedItem == null)
        {
            MessageBox.Show("Выберите запись для изменения.");
            return;
        }

        var w = new GenericRecordWindow(id, dgMain.SelectedItem) { Owner = this };
        if (w.ShowDialog() == true)
        {
            Load();
            MessageBox.Show("Запись изменена");
        }
    }

    private void GenericDelete()
    {
        if (_currentTableId is not { } id || !CrudRegistry.TryGet(id, out var spec))
            return;

        if (dgMain.SelectedItem is not { } selected)
        {
            MessageBox.Show("Выберите запись для удаления.");
            return;
        }

        var label = GenericCrudService.GetDisplayText(spec, selected);
        var result = MessageBox.Show(
            $"Удалить «{label}»?",
            "Подтверждение удаления",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            GenericCrudService.Delete(spec, selected);
            Load();
            MessageBox.Show("Удалено");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message);
        }
    }
}
