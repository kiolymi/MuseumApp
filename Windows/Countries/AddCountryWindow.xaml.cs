using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Countries;

public partial class AddCountryWindow : Window
{
    public AddCountryWindow()
    {
        InitializeComponent();
    }

    private string? ValidateForm()
    {
        return ValidationHelper.SafeText(txtValue.Text, 255, "Название");
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
            context.Countries.Add(new Country { CountryName = txtValue.Text.Trim() });
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
