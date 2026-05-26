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
            var error = ValidationHelper.First(
                ValidationHelper.NotEmpty(txtLastName.Text, "Фамилия"),
                ValidationHelper.NotEmpty(txtFirstName.Text, "Имя"),
                ValidationHelper.MaxLen(txtLastName.Text, 45, "Фамилия"),
                ValidationHelper.MaxLen(txtFirstName.Text, 45, "Имя"),
                ValidationHelper.MaxLen(txtMiddleName.Text, 45, "Отчество"),
                ValidationHelper.Combo(cmbCountry.SelectedValue, "Страна"));
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var context = new MuseumDbContext();
            var author = new Author
            {
                LastName = txtLastName.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                MiddleName = string.IsNullOrWhiteSpace(txtMiddleName.Text) ? null : txtMiddleName.Text.Trim(),
                IdCountry = (int)cmbCountry.SelectedValue!
            };
            context.Authors.Add(author);
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
