using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Privileges;

public partial class AddPrivilegeWindow : Window
{
    public AddPrivilegeWindow()
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

        var discount = string.IsNullOrWhiteSpace(txtDiscount.Text)
            ? null
            : InputHelper.ParseNullableDouble(txtDiscount.Text);

        error = ValidationHelper.DiscountPercent(discount);
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

            var discount = string.IsNullOrWhiteSpace(txtDiscount.Text)
                ? null
                : InputHelper.ParseNullableDouble(txtDiscount.Text);

            var context = new MuseumDbContext();
            var privilege = new Privilege
            {
                PrivilegeName = txtName.Text.Trim(),
                DiscountRate = discount
            };
            context.Privileges.Add(privilege);
            context.SaveChanges();
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(DbExceptionHelper.GetMessage(ex));
        }
    }
}
