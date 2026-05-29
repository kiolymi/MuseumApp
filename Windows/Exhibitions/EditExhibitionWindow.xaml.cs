using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Exhibitions;

public partial class EditExhibitionWindow : Window
{
    private readonly int _id;

    public EditExhibitionWindow(Exhibition selected)
    {
        InitializeComponent();
        _id = selected.IdExhibition;
        txtName.Text = selected.ExhibitionName;
        dpStart.SelectedDate = selected.StartDate;
        dpEnd.SelectedDate = selected.EndDate;
        txtPrice.Text = selected.Price.ToString();
        txtTheme.Text = selected.Theme ?? "";

        ComboLoadHelper.LoadEmployees(cmbCurator, selected.IdCurator);
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Dates(dpStart.SelectedDate, dpEnd.SelectedDate);
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbCurator.SelectedValue, "Куратор");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtTheme.Text, 255, "Тема");
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

            var context = new MuseumDbContext();
            var item = context.Exhibitions.Find(_id);
            if (item == null)
            {
                return;
            }

            item.ExhibitionName = txtName.Text.Trim();
            item.StartDate = dpStart.SelectedDate!.Value;
            item.EndDate = dpEnd.SelectedDate!.Value;
            item.Price = price;
            item.Theme = TextHelper.TrimOrNull(txtTheme.Text);
            item.IdCurator = (int)cmbCurator.SelectedValue!;

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
