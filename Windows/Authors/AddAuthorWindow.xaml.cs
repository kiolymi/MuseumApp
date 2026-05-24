using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Authors;

public partial class AddAuthorWindow : Window
{
    public AddAuthorWindow()
    {
        InitializeComponent();
        var context = new MuseumDbContext();
        cmbCountry.ItemsSource = context.Countries
            .Select(c => new { Id = c.IdCountry, Name = c.CountryName })
            .ToList();
        if (cmbCountry.Items.Count > 0)
            cmbCountry.SelectedIndex = 0;
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
