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
            var startTime = TimeOnly.Parse(txtStartTime.Text.Trim(), CultureInfo.InvariantCulture);
            var context = new MuseumDbContext();
            var ev = new Event
            {
                EventName = txtName.Text,
                IdBranch = (int)cmbBranch.SelectedValue,
                IdEmployee = (int)cmbEmployee.SelectedValue,
                EventDate = DateOnly.FromDateTime(dpEventDate.SelectedDate ?? DateTime.Today),
                StartTime = startTime,
                DurationMinutes = Convert.ToInt32(txtDuration.Text),
                Price = Convert.ToDecimal(txtPrice.Text)
            };
            context.Events.Add(ev);
            context.SaveChanges();
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
