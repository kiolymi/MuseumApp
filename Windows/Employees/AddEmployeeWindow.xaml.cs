using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Employees;

public partial class AddEmployeeWindow : Window
{
    public AddEmployeeWindow()
    {
        InitializeComponent();
        dpBirthDate.SelectedDate = DateTime.Today.AddYears(-30);

        var context = new MuseumDbContext();
        cmbPosition.ItemsSource = context.Positions
            .Select(p => new { Id = p.IdPosition, Name = p.PositionName })
            .ToList();
        if (cmbPosition.Items.Count > 0)
            cmbPosition.SelectedIndex = 0;
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var employee = new Employee
            {
                LastName = txtLastName.Text,
                FirstName = txtFirstName.Text,
                MiddleName = txtMiddleName.Text,
                IdPosition = (int)cmbPosition.SelectedValue,
                Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text,
                BirthDate = DateOnly.FromDateTime(dpBirthDate.SelectedDate ?? DateTime.Today),
                EducationLevel = string.IsNullOrWhiteSpace(txtEducation.Text) ? null : txtEducation.Text
            };
            context.Employees.Add(employee);
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
