using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Authors;

public partial class EditAuthorWindow : Window
{
    private readonly int _id;

    public EditAuthorWindow(Author selected)
    {
        InitializeComponent();
        _id = selected.IdAuthor;
        txtLastName.Text = selected.LastName;
        txtFirstName.Text = selected.FirstName;
        txtMiddleName.Text = selected.MiddleName ?? "";

        var context = new MuseumDbContext();
        cmbCountry.ItemsSource = context.Countries
            .Select(c => new { Id = c.IdCountry, Name = c.CountryName })
            .ToList();
        cmbCountry.SelectedValue = selected.IdCountry;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var item = context.Authors.Find(_id);
            if (item == null) return;

            item.LastName = txtLastName.Text;
            item.FirstName = txtFirstName.Text;
            item.MiddleName = string.IsNullOrWhiteSpace(txtMiddleName.Text) ? null : txtMiddleName.Text;
            item.IdCountry = (int)cmbCountry.SelectedValue;

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
