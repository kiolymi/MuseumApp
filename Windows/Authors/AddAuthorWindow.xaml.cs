using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Authors;

public partial class AddAuthorWindow : Window
{
    public AddAuthorWindow()
    {
        InitializeComponent();
        if (!ComboLoadHelper.TryLoadCountriesForAdd(cmbCountry))
            Close();
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var author = new Author
            {
                LastName = txtLastName.Text,
                FirstName = txtFirstName.Text,
                MiddleName = string.IsNullOrWhiteSpace(txtMiddleName.Text) ? null : txtMiddleName.Text,
                IdCountry = (int)cmbCountry.SelectedValue
            };
            context.Authors.Add(author);
            context.SaveChanges();
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
