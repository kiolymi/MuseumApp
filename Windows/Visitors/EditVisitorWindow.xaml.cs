using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

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
            DbErrorHelper.Show(ex);
            Close();
        }
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var error = ValidationHelper.First(
                ValidationHelper.NotEmpty(txtLastName.Text, "Фамилия"),
                ValidationHelper.NotEmpty(txtFirstName.Text, "Имя"),
                ValidationHelper.NotEmpty(txtMiddleName.Text, "Отчество"),
                ValidationHelper.MaxLen(txtLastName.Text, 45, "Фамилия"),
                ValidationHelper.MaxLen(txtFirstName.Text, 45, "Имя"),
                ValidationHelper.MaxLen(txtMiddleName.Text, 45, "Отчество"),
                ValidationHelper.MaxLen(txtPhone.Text, 45, "Телефон"),
                ValidationHelper.MaxLen(txtEmail.Text, 100, "Email"));
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var context = new MuseumDbContext();
            var item = context.Visitors.Find(_id);
            if (item == null) return;

            item.LastName = txtLastName.Text.Trim();
            item.FirstName = txtFirstName.Text.Trim();
            item.MiddleName = txtMiddleName.Text.Trim();
            item.Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text;
            item.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;
            item.IdPrivilege = cmbPrivilege.SelectedValue as int?;

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
