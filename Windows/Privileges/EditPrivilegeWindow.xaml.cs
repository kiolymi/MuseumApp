using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

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

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var item = context.Privileges.Find(_id);
            if (item == null) return;

            item.PrivilegeName = txtName.Text;
            item.DiscountRate = string.IsNullOrWhiteSpace(txtDiscount.Text)
                ? null
                : Convert.ToDouble(txtDiscount.Text);

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
