using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Visitors;

public partial class EditVisitorWindow : Window
{
    private readonly int _id;

    public EditVisitorWindow(Visitor selected)
    {
        InitializeComponent();
        _id = selected.IdVisitor;
        txtLastName.Text = selected.LastName;
        txtFirstName.Text = selected.FirstName;
        txtMiddleName.Text = selected.MiddleName;
        txtPhone.Text = selected.Phone ?? "";
        txtEmail.Text = selected.Email ?? "";

        try
        {
            var context = new MuseumDbContext();
            var privileges = context.Privileges
                .Select(p => new { Id = (int?)p.IdPrivilege, Name = p.PrivilegeName })
                .ToList();
            privileges.Insert(0, new { Id = (int?)null, Name = "(без льготы)" });
            cmbPrivilege.ItemsSource = privileges;
            cmbPrivilege.SelectedValue = selected.IdPrivilege;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка загрузки: " + ex.Message);
            Close();
        }
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var item = context.Visitors.Find(_id);
            if (item == null) return;

            item.LastName = txtLastName.Text;
            item.FirstName = txtFirstName.Text;
            item.MiddleName = txtMiddleName.Text;
            item.Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text;
            item.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;
            item.IdPrivilege = cmbPrivilege.SelectedValue as int?;

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
