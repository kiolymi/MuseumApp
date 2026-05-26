using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

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

        ComboLoadHelper.LoadCountries(cmbCountry, selected.IdCountry);
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
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
            var item = context.Authors.Find(_id);
            if (item == null) return;

            item.LastName = txtLastName.Text.Trim();
            item.FirstName = txtFirstName.Text.Trim();
            item.MiddleName = string.IsNullOrWhiteSpace(txtMiddleName.Text) ? null : txtMiddleName.Text.Trim();
            item.IdCountry = (int)cmbCountry.SelectedValue!;

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
