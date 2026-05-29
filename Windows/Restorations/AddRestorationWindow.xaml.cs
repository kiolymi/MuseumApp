using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Restorations;

public partial class AddRestorationWindow : Window
{
    public AddRestorationWindow()
    {
        InitializeComponent();
        dpStart.SelectedDate = DateTime.Today;

        if (!ComboLoadHelper.TryLoadExhibitsForAdd(cmbExhibit)
            || !ComboLoadHelper.TryLoadEmployeesForAdd(cmbRestorer))
        {
            Close();
        }
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.Combo(cmbExhibit.SelectedValue, "Экспонат");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbRestorer.SelectedValue, "Реставратор");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtDesc.Text, 255, "Описание работ");
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

            decimal? cost = string.IsNullOrWhiteSpace(txtCost.Text)
                ? null
                : InputHelper.ParseDecimal(txtCost.Text);

            using var context = new MuseumDbContext();
            context.Restorations.Add(new Restoration
            {
                IdExhibit = (int)cmbExhibit.SelectedValue!,
                IdRestorer = (int)cmbRestorer.SelectedValue!,
                StartDate = dpStart.SelectedDate ?? DateTime.Today,
                EndDate = dpEnd.SelectedDate,
                Cost = cost,
                WorkDescription = TextHelper.TrimOrNull(txtDesc.Text)
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
