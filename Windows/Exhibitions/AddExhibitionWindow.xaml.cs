using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Exhibitions;

public partial class AddExhibitionWindow : Window
{
    public AddExhibitionWindow()
    {
        InitializeComponent();
        dpStart.SelectedDate = DateTime.Today;
        dpEnd.SelectedDate = DateTime.Today.AddDays(7);

        if (!ComboLoadHelper.TryLoadEmployeesForAdd(cmbCurator))
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

            var context = new MuseumDbContext();
            var exhibition = new Exhibition
            {
                IdExhibition = (context.Exhibitions.Max(x => (int?)x.IdExhibition) ?? 0) + 1,
                ExhibitionName = txtName.Text.Trim(),
                StartDate = dpStart.SelectedDate!.Value,
                EndDate = dpEnd.SelectedDate!.Value,
                Price = price,
                Theme = TextHelper.TrimOrNull(txtTheme.Text),
                IdCurator = (int)cmbCurator.SelectedValue!
            };
            context.Exhibitions.Add(exhibition);
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
