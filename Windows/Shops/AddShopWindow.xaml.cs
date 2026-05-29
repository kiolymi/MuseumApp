using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Shops;

public partial class AddShopWindow : Window
{
    public AddShopWindow()
    {
        InitializeComponent();
        if (!ComboLoadHelper.TryLoadBranchesForAdd(cmbBranch))
        {
            Close();
        }
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.Combo(cmbBranch.SelectedValue, "Филиал");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
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

        error = ValidationHelper.OptionalSafeText(txtHours.Text, 255, "Часы работы");
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
            context.Shops.Add(new Shop
            {
                IdBranch = (int)cmbBranch.SelectedValue!,
                ShopName = txtName.Text.Trim(),
                Phone = TextHelper.TrimOrNull(txtPhone.Text),
                Email = TextHelper.TrimOrNull(txtEmail.Text),
                WorkingHours = TextHelper.TrimOrNull(txtHours.Text)
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
