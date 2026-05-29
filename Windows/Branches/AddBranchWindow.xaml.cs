using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Branches;

public partial class AddBranchWindow : Window
{
    public AddBranchWindow()
    {
        InitializeComponent();
        if (!ComboLoadHelper.TryLoadMuseumsForAdd(cmbMuseum)
            || !ComboLoadHelper.TryLoadAdressesForAdd(cmbAddress)
            || !ComboLoadHelper.TryLoadEmployeesForAdd(cmbResponsible))
        {
            Close();
        }
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

            using var context = new MuseumDbContext();
            context.Branches.Add(new Branch
            {
                IdMuseum = (int)cmbMuseum.SelectedValue!,
                BranchName = txtName.Text.Trim(),
                IdAddress = (int)cmbAddress.SelectedValue!,
                Phone = TextHelper.TrimOrNull(txtPhone.Text),
                IdResponsible = (int)cmbResponsible.SelectedValue!
            });
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
