using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Employees;

public partial class AddEmployeeWindow : Window
{
    public AddEmployeeWindow()
    {
        InitializeComponent();
        dpBirthDate.SelectedDate = DateTime.Today.AddYears(-30);

        if (!ComboLoadHelper.TryLoadPositionsForAdd(cmbPosition))
            Close();
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var error = ValidationHelper.First(
                ValidationHelper.NotEmpty(txtLastName.Text, "Фамилия"),
                ValidationHelper.NotEmpty(txtFirstName.Text, "Имя"),
                ValidationHelper.NotEmpty(txtMiddleName.Text, "Отчество"),
                ValidationHelper.MaxLen(txtLastName.Text, 45, "Фамилия"),
                ValidationHelper.MaxLen(txtFirstName.Text, 45, "Имя"),
                ValidationHelper.MaxLen(txtMiddleName.Text, 45, "Отчество"),
                ValidationHelper.MaxLen(txtPhone.Text, 45, "Телефон"),
                ValidationHelper.MaxLen(txtEducation.Text, 45, "Образование"),
                ValidationHelper.Combo(cmbPosition.SelectedValue, "Должность"),
                ValidationHelper.BirthDate(dpBirthDate.SelectedDate));
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var context = new MuseumDbContext();
            var employee = new Employee
            {
                LastName = txtLastName.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                MiddleName = txtMiddleName.Text.Trim(),
                IdPosition = (int)cmbPosition.SelectedValue!,
                Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text,
                BirthDate = DateOnly.FromDateTime(dpBirthDate.SelectedDate!.Value),
                EducationLevel = string.IsNullOrWhiteSpace(txtEducation.Text) ? null : txtEducation.Text.Trim()
            };
            context.Employees.Add(employee);
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
