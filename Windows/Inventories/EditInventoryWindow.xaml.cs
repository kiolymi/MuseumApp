using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Inventories;

public partial class EditInventoryWindow : Window
{
    private readonly int _shop;
    private readonly int _product;

    public EditInventoryWindow(Inventory inventory)
    {
        InitializeComponent();
        _shop = inventory.IdShop;
        _product = inventory.IdProduct;
        txtQty.Text = inventory.Quantity.ToString();

        ComboLoadHelper.LoadShops(cmbShop, inventory.IdShop);
        ComboLoadHelper.LoadProducts(cmbProduct, inventory.IdProduct);
    }

    private string? ValidateForm()
    {
        var qty = InputHelper.ParseInt(txtQty.Text);
        return ValidationHelper.NonNegative(qty, "Количество");
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

            var qty = InputHelper.ParseInt(txtQty.Text);

            using var context = new MuseumDbContext();
            var item = context.Inventories.Find(_shop, _product);
            if (item == null)
            {
                return;
            }

            item.Quantity = qty;

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
