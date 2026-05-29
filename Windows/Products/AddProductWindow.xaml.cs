using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Products;

public partial class AddProductWindow : Window
{
    public AddProductWindow()
    {
        InitializeComponent();
        if (!ComboLoadHelper.TryLoadCompaniesForAdd(cmbCompany))
        {
            Close();
        }
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtDesc.Text, 255, "Описание");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbCompany.SelectedValue, "Поставщик");
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

            var price = InputHelper.ParseDecimal(txtPrice.Text);
            var priceError = ValidationHelper.NonNegative(price, "Цена");
            if (priceError != null)
            {
                MessageBox.Show(priceError);
                return;
            }

            using var context = new MuseumDbContext();
            context.Products.Add(new Product
            {
                ProductName = txtName.Text.Trim(),
                Description = TextHelper.TrimOrNull(txtDesc.Text),
                Price = price,
                IdCompanySupplier = (int)cmbCompany.SelectedValue!
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
