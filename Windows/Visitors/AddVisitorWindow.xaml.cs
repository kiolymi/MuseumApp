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

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtLastName.Text, 45, "Фамилия");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.SafeText(txtFirstName.Text, 45, "Имя");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.SafeText(txtMiddleName.Text, 45, "Отчество");
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

            var context = new MuseumDbContext();
            var visitor = new Visitor
            {
                LastName = txtLastName.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                MiddleName = txtMiddleName.Text.Trim(),
                Phone = TextHelper.TrimOrNull(txtPhone.Text),
                Email = TextHelper.TrimOrNull(txtEmail.Text),
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
