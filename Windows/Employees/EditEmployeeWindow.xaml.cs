using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Employees;

public partial class EditEmployeeWindow : Window
{
    private readonly int _id;

    public EditEmployeeWindow(Employee selected)
    {
        InitializeComponent();
        _id = selected.IdEmployee;
        txtLastName.Text = selected.LastName;
        txtFirstName.Text = selected.FirstName;
        txtMiddleName.Text = selected.MiddleName;
        txtPhone.Text = selected.Phone ?? "";
        txtEducation.Text = selected.EducationLevel ?? "";
        dpBirthDate.SelectedDate = selected.BirthDate.ToDateTime(TimeOnly.MinValue);

        ComboLoadHelper.LoadPositions(cmbPosition, selected.IdPosition);
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var item = context.Employees.Find(_id);
            if (item == null) return;

            item.LastName = txtLastName.Text;
            item.FirstName = txtFirstName.Text;
            item.MiddleName = txtMiddleName.Text;
            item.IdPosition = (int)cmbPosition.SelectedValue;
            item.Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text;
            item.BirthDate = DateOnly.FromDateTime(dpBirthDate.SelectedDate ?? DateTime.Today);
            item.EducationLevel = string.IsNullOrWhiteSpace(txtEducation.Text) ? null : txtEducation.Text;

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
