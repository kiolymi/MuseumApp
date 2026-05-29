using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Adresses;

public partial class EditAdressWindow : Window
{
    private readonly int _id;

    public EditAdressWindow(Adress address)
    {
        InitializeComponent();
        _id = address.IdAddress;
        txtCity.Text = address.City;
        txtStreet.Text = address.Street;
        txtHouse.Text = address.House;
        txtPostal.Text = address.PostalCode ?? "";
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
            var item = context.Adresses.Find(_id);
            if (item == null)
            {
                return;
            }

            item.City = txtCity.Text.Trim();
            item.Street = txtStreet.Text.Trim();
            item.House = txtHouse.Text.Trim();
            item.PostalCode = TextHelper.TrimOrNull(txtPostal.Text);

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
