using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Storages;

public partial class EditStorageWindow : Window
{
    private readonly int _id;

    public EditStorageWindow(Storage storage)
    {
        InitializeComponent();
        _id = storage.IdStorage;
        txtName.Text = storage.StorageName;
        txtTemp.Text = storage.Temperature?.ToString() ?? "";
        txtHumidity.Text = storage.Humidity?.ToString() ?? "";

        ComboLoadHelper.LoadBranches(cmbBranch, storage.IdBranch);
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbBranch.SelectedValue, "Филиал");
        if (error != null)
        {
            return error;
        }

        return null;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var error = ValidateForm();
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            decimal? temperature = string.IsNullOrWhiteSpace(txtTemp.Text)
                ? null
                : InputHelper.ParseDecimal(txtTemp.Text);
            decimal? humidity = string.IsNullOrWhiteSpace(txtHumidity.Text)
                ? null
                : InputHelper.ParseDecimal(txtHumidity.Text);

            using var context = new MuseumDbContext();
            var item = context.Storages.Find(_id);
            if (item == null)
            {
                return;
            }

            item.StorageName = txtName.Text.Trim();
            item.IdBranch = (int)cmbBranch.SelectedValue!;
            item.Temperature = temperature;
            item.Humidity = humidity;

            context.SaveChanges();
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
        }
    }
}
