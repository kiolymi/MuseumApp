using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Museums;

public partial class AddMuseumWindow : Window
{
    public AddMuseumWindow()
    {
        InitializeComponent();
        if (!ComboLoadHelper.TryLoadEmployeesForAdd(cmbDirector)
            || !ComboLoadHelper.TryLoadAdressesForAdd(cmbAddress))
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

        error = ValidationHelper.Combo(cmbDirector.SelectedValue, "Директор");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbAddress.SelectedValue, "Адрес");
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

            using var context = new MuseumDbContext();
            context.Museums.Add(new Museum
            {
                Name = txtName.Text.Trim(),
                IdDirector = (int)cmbDirector.SelectedValue!,
                IdAddress = (int)cmbAddress.SelectedValue!
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
