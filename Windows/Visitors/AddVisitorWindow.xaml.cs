using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

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
            MessageBox.Show("Ошибка загрузки: " + ex.Message);
            Close();
        }
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var visitor = new Visitor
            {
                LastName = txtLastName.Text,
                FirstName = txtFirstName.Text,
                MiddleName = txtMiddleName.Text,
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
            MessageBox.Show(ex.Message);
        }
    }
}
