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
            Close();
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
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
