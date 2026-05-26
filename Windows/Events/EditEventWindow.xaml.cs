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

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var duration = InputHelper.ParseInt(txtDuration.Text);
            var price = InputHelper.ParseDecimal(txtPrice.Text);
            var error = ValidationHelper.First(
                ValidationHelper.NotEmpty(txtName.Text, "Название"),
                ValidationHelper.MaxLen(txtName.Text, 255, "Название"),
                ValidationHelper.Combo(cmbBranch.SelectedValue, "Филиал"),
                ValidationHelper.Combo(cmbEmployee.SelectedValue, "Сотрудник"),
                ValidationHelper.VisitDate(dpEventDate.SelectedDate),
                ValidationHelper.Positive(duration, "Длительность"),
                ValidationHelper.NonNegative(price, "Цена"));
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var startTime = TimeOnly.Parse(txtStartTime.Text.Trim(), CultureInfo.InvariantCulture);
            var context = new MuseumDbContext();
            var item = context.Events.Find(_id);
            if (item == null) return;

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
