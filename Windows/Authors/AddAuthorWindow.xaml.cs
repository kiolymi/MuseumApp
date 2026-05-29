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
        {
            Close();
        }
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtLastName.Text, 45, "Фамилия");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.SafeText(txtFirstName.Text, 45, "Имя");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtMiddleName.Text, 45, "Отчество");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbCountry.SelectedValue, "Страна");
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

            var context = new MuseumDbContext();
            var author = new Author
            {
                LastName = txtLastName.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                MiddleName = TextHelper.TrimOrNull(txtMiddleName.Text),
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
