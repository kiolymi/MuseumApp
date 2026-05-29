using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Products;

public partial class EditProductWindow : Window
{
    private readonly int _id;

    public EditProductWindow(Product product)
    {
        InitializeComponent();
        _id = product.IdProduct;
        txtName.Text = product.ProductName;
        txtDesc.Text = product.Description ?? "";
        txtPrice.Text = product.Price.ToString();

        ComboLoadHelper.LoadCompanies(cmbCompany, product.IdCompanySupplier);
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

            var price = InputHelper.ParseDecimal(txtPrice.Text);
            var priceError = ValidationHelper.NonNegative(price, "Цена");
            if (priceError != null)
            {
                MessageBox.Show(priceError);
                return;
            }

            using var context = new MuseumDbContext();
            var item = context.Products.Find(_id);
            if (item == null)
            {
                return;
            }

            item.ProductName = txtName.Text.Trim();
            item.Description = TextHelper.TrimOrNull(txtDesc.Text);
            item.Price = price;
            item.IdCompanySupplier = (int)cmbCompany.SelectedValue!;

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
