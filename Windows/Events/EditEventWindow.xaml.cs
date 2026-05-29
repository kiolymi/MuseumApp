using System.Globalization;
using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Events;

public partial class EditEventWindow : Window
{
    private readonly int _id;

    public EditEventWindow(Event selected)
    {
        InitializeComponent();
        _id = selected.IdEvent;
        txtName.Text = selected.EventName;
        dpEventDate.SelectedDate = selected.EventDate.ToDateTime(TimeOnly.MinValue);
        txtStartTime.Text = selected.StartTime.ToString("HH:mm", CultureInfo.InvariantCulture);
        txtDuration.Text = selected.DurationMinutes.ToString();
        txtPrice.Text = selected.Price.ToString();

        ComboLoadHelper.LoadBranches(cmbBranch, selected.IdBranch);
        ComboLoadHelper.LoadEmployees(cmbEmployee, selected.IdEmployee);
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
            var item = context.Events.Find(_id);
            if (item == null)
            {
                return;
            }

            item.EventName = txtName.Text.Trim();
            item.IdBranch = (int)cmbBranch.SelectedValue!;
            item.IdEmployee = (int)cmbEmployee.SelectedValue!;
            item.EventDate = DateOnly.FromDateTime(dpEventDate.SelectedDate!.Value);
            item.StartTime = startTime;
            item.DurationMinutes = duration;
            item.Price = price;

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
