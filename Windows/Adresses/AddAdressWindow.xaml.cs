using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Adresses;

public partial class AddAdressWindow : Window
{
    public AddAdressWindow()
    {
        InitializeComponent();
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtCity.Text, 45, "Город");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.SafeText(txtStreet.Text, 45, "Улица");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.SafeText(txtHouse.Text, 45, "Дом");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtPostal.Text, 45, "Индекс");
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
            context.Adresses.Add(new Adress
            {
                City = txtCity.Text.Trim(),
                Street = txtStreet.Text.Trim(),
                House = txtHouse.Text.Trim(),
                PostalCode = TextHelper.TrimOrNull(txtPostal.Text)
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
