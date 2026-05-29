using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Museums;

public partial class EditMuseumWindow : Window
{
    private readonly int _id;

    public EditMuseumWindow(Museum museum)
    {
        InitializeComponent();
        _id = museum.IdMuseum;
        txtName.Text = museum.Name;

        ComboLoadHelper.LoadEmployees(cmbDirector, museum.IdDirector);
        ComboLoadHelper.LoadAdresses(cmbAddress, museum.IdAddress);
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

            using var context = new MuseumDbContext();
            var item = context.Museums.Find(_id);
            if (item == null)
            {
                return;
            }

            item.Name = txtName.Text.Trim();
            item.IdDirector = (int)cmbDirector.SelectedValue!;
            item.IdAddress = (int)cmbAddress.SelectedValue!;

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
