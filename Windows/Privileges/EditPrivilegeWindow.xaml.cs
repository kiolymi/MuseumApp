using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Privileges;

public partial class EditPrivilegeWindow : Window
{
    private readonly int _id;

    public EditPrivilegeWindow(Privilege selected)
    {
        InitializeComponent();
        _id = selected.IdPrivilege;
        txtName.Text = selected.PrivilegeName;
        txtDiscount.Text = selected.DiscountRate?.ToString() ?? "";
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

            var discount = string.IsNullOrWhiteSpace(txtDiscount.Text)
                ? null
                : InputHelper.ParseNullableDouble(txtDiscount.Text);

            var context = new MuseumDbContext();
            var item = context.Privileges.Find(_id);
            if (item == null)
            {
                return;
            }

            item.PrivilegeName = txtName.Text.Trim();
            item.DiscountRate = discount;

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
