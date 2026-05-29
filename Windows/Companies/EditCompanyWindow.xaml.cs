using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Companies;

public partial class EditCompanyWindow : Window
{
    private readonly int _id;

    public EditCompanyWindow(Company company)
    {
        InitializeComponent();
        _id = company.IdCompany;
        txtName.Text = company.CompanyName;
        txtInn.Text = company.Inn ?? "";
        txtAddress.Text = company.LegalAddress ?? "";
        txtPhone.Text = company.ContactPhone ?? "";
        txtEmail.Text = company.ContactEmail ?? "";
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
            var item = context.Companies.Find(_id);
            if (item == null)
            {
                return;
            }

            item.CompanyName = txtName.Text.Trim();
            item.Inn = TextHelper.TrimOrNull(txtInn.Text);
            item.LegalAddress = TextHelper.TrimOrNull(txtAddress.Text);
            item.ContactPhone = TextHelper.TrimOrNull(txtPhone.Text);
            item.ContactEmail = TextHelper.TrimOrNull(txtEmail.Text);

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
