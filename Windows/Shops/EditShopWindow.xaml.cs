using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Shops;

public partial class EditShopWindow : Window
{
    private readonly int _id;

    public EditShopWindow(Shop shop)
    {
        InitializeComponent();
        _id = shop.IdShop;
        txtName.Text = shop.ShopName;
        txtPhone.Text = shop.Phone ?? "";
        txtEmail.Text = shop.Email ?? "";
        txtHours.Text = shop.WorkingHours ?? "";

        ComboLoadHelper.LoadBranches(cmbBranch, shop.IdBranch);
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
            var item = context.Shops.Find(_id);
            if (item == null)
            {
                return;
            }

            item.IdBranch = (int)cmbBranch.SelectedValue!;
            item.ShopName = txtName.Text.Trim();
            item.Phone = TextHelper.TrimOrNull(txtPhone.Text);
            item.Email = TextHelper.TrimOrNull(txtEmail.Text);
            item.WorkingHours = TextHelper.TrimOrNull(txtHours.Text);

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
