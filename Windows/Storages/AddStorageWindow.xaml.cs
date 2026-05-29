using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Storages;

public partial class AddStorageWindow : Window
{
    public AddStorageWindow()
    {
        InitializeComponent();
        if (!ComboLoadHelper.TryLoadBranchesForAdd(cmbBranch))
        {
            Close();
        }
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

    private void btnAdd_Click(object sender, RoutedEventArgs e)
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
            context.Storages.Add(new Storage
            {
                StorageName = txtName.Text.Trim(),
                IdBranch = (int)cmbBranch.SelectedValue!,
                Temperature = temperature,
                Humidity = humidity
            });
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
