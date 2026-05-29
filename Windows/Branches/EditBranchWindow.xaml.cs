using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Branches;

public partial class EditBranchWindow : Window
{
    private readonly int _id;

    public EditBranchWindow(Branch branch)
    {
        InitializeComponent();
        _id = branch.IdBranch;
        txtName.Text = branch.BranchName;
        txtPhone.Text = branch.Phone ?? "";

        ComboLoadHelper.LoadMuseums(cmbMuseum, branch.IdMuseum);
        ComboLoadHelper.LoadAdresses(cmbAddress, branch.IdAddress);
        ComboLoadHelper.LoadEmployees(cmbResponsible, branch.IdResponsible);
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.Combo(cmbMuseum.SelectedValue, "Музей");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbAddress.SelectedValue, "Адрес");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbResponsible.SelectedValue, "Ответственный");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Phone(txtPhone.Text, "Телефон", required: false);
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

            using var context = new MuseumDbContext();
            var item = context.Branches.Find(_id);
            if (item == null)
            {
                return;
            }

            item.IdMuseum = (int)cmbMuseum.SelectedValue!;
            item.BranchName = txtName.Text.Trim();
            item.IdAddress = (int)cmbAddress.SelectedValue!;
            item.Phone = TextHelper.TrimOrNull(txtPhone.Text);
            item.IdResponsible = (int)cmbResponsible.SelectedValue!;

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
