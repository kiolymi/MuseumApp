using System.Globalization;
using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Events;

public partial class AddEventWindow : Window
{
    public AddEventWindow()
    {
        InitializeComponent();
        dpEventDate.SelectedDate = DateTime.Today;

        if (!ComboLoadHelper.TryLoadBranchesForAdd(cmbBranch))
        {
            Close();
            return;
        }

        if (!ComboLoadHelper.TryLoadEmployeesForAdd(cmbEmployee))
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

        error = ValidationHelper.Combo(cmbBranch.SelectedValue, "Филиал");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbEmployee.SelectedValue, "Сотрудник");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.VisitDate(dpEventDate.SelectedDate);
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

            var duration = InputHelper.ParseInt(txtDuration.Text);
            var durationError = ValidationHelper.Positive(duration, "Длительность");
            if (durationError != null)
            {
                MessageBox.Show(durationError);
                return;
            }

            var price = InputHelper.ParseDecimal(txtPrice.Text);
            var priceError = ValidationHelper.NonNegative(price, "Цена");
            if (priceError != null)
            {
                MessageBox.Show(priceError);
                return;
            }

            var startTime = TimeOnly.Parse(txtStartTime.Text.Trim(), CultureInfo.InvariantCulture);
            var context = new MuseumDbContext();
            var ev = new Event
            {
                EventName = txtName.Text.Trim(),
                IdBranch = (int)cmbBranch.SelectedValue!,
                IdEmployee = (int)cmbEmployee.SelectedValue!,
                EventDate = DateOnly.FromDateTime(dpEventDate.SelectedDate!.Value),
                StartTime = startTime,
                DurationMinutes = duration,
                Price = price
            };
            context.Events.Add(ev);
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
