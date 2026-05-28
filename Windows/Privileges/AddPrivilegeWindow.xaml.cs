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
            var context = new MuseumDbContext();
            var privilege = new Privilege
            {
                PrivilegeName = txtName.Text,
                DiscountRate = string.IsNullOrWhiteSpace(txtDiscount.Text)
                    ? null
                    : Convert.ToDouble(txtDiscount.Text)
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
