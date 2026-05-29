using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Companies;

public partial class AddCompanyWindow : Window
{
    public AddCompanyWindow()
    {
        InitializeComponent();
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtAddress.Text, 255, "Юридический адрес");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Inn(txtInn.Text, "ИНН");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Phone(txtPhone.Text, "Телефон", required: false);
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Email(txtEmail.Text, "Email", required: false);
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
            context.Companies.Add(new Company
            {
                CompanyName = txtName.Text.Trim(),
                Inn = TextHelper.TrimOrNull(txtInn.Text),
                LegalAddress = TextHelper.TrimOrNull(txtAddress.Text),
                ContactPhone = TextHelper.TrimOrNull(txtPhone.Text),
                ContactEmail = TextHelper.TrimOrNull(txtEmail.Text)
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
