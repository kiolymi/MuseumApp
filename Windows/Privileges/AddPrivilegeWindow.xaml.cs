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

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var discount = string.IsNullOrWhiteSpace(txtDiscount.Text)
                ? null
                : InputHelper.ParseNullableDouble(txtDiscount.Text);
            var error = ValidationHelper.First(
                ValidationHelper.NotEmpty(txtName.Text, "Название"),
                ValidationHelper.MaxLen(txtName.Text, 255, "Название"),
                ValidationHelper.DiscountPercent(discount));
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

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
