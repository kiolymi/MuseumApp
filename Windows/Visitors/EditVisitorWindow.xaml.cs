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

            var context = new MuseumDbContext();
            var item = context.Visitors.Find(_id);
            if (item == null)
            {
                return;
            }

            item.LastName = txtLastName.Text.Trim();
            item.FirstName = txtFirstName.Text.Trim();
            item.MiddleName = txtMiddleName.Text.Trim();
            item.Phone = TextHelper.TrimOrNull(txtPhone.Text);
            item.Email = TextHelper.TrimOrNull(txtEmail.Text);
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
