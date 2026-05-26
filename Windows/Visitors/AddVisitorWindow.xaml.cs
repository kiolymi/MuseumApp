using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Visitors;

public partial class AddVisitorWindow : Window
{
    public AddVisitorWindow()
    {
        InitializeComponent();

        try
        {
            var context = new MuseumDbContext();
            var privileges = context.Privileges
                .Select(p => new { Id = (int?)p.IdPrivilege, Name = p.PrivilegeName })
                .ToList();
            privileges.Insert(0, new { Id = (int?)null, Name = "(без льготы)" });
            cmbPrivilege.ItemsSource = privileges;
            cmbPrivilege.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
            Close();
        }
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
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
            var visitor = new Visitor
            {
                LastName = txtLastName.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                MiddleName = txtMiddleName.Text.Trim(),
                Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text,
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text,
                IdPrivilege = cmbPrivilege.SelectedValue as int?
            };
            context.Visitors.Add(visitor);
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
