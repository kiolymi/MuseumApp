using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Inventories;

public partial class AddInventoryWindow : Window
{
    public AddInventoryWindow()
    {
        InitializeComponent();
        if (!ComboLoadHelper.TryLoadShopsForAdd(cmbShop)
            || !ComboLoadHelper.TryLoadProductsForAdd(cmbProduct))
        {
            Close();
        }
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.Combo(cmbShop.SelectedValue, "Магазин");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbProduct.SelectedValue, "Товар");
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

            var qty = InputHelper.ParseInt(txtQty.Text);
            var qtyError = ValidationHelper.NonNegative(qty, "Количество");
            if (qtyError != null)
            {
                MessageBox.Show(qtyError);
                return;
            }

            using var context = new MuseumDbContext();
            context.Inventories.Add(new Inventory
            {
                IdShop = (int)cmbShop.SelectedValue!,
                IdProduct = (int)cmbProduct.SelectedValue!,
                Quantity = qty
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
