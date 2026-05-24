using System.Globalization;
using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

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

        var context = new MuseumDbContext();
        cmbBranch.ItemsSource = context.Branches
            .Select(b => new { Id = b.IdBranch, Name = b.BranchName })
            .ToList();
        cmbEmployee.ItemsSource = context.Employees
            .Select(e => new { Id = e.IdEmployee, Name = $"{e.LastName} {e.FirstName}" })
            .ToList();
        cmbBranch.SelectedValue = selected.IdBranch;
        cmbEmployee.SelectedValue = selected.IdEmployee;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var startTime = TimeOnly.Parse(txtStartTime.Text.Trim(), CultureInfo.InvariantCulture);
            var context = new MuseumDbContext();
            var item = context.Events.Find(_id);
            if (item == null) return;

            item.EventName = txtName.Text;
            item.IdBranch = (int)cmbBranch.SelectedValue;
            item.IdEmployee = (int)cmbEmployee.SelectedValue;
            item.EventDate = DateOnly.FromDateTime(dpEventDate.SelectedDate ?? DateTime.Today);
            item.StartTime = startTime;
            item.DurationMinutes = Convert.ToInt32(txtDuration.Text);
            item.Price = Convert.ToDecimal(txtPrice.Text);

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
