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
        {
            Close();
        }
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtLastName.Text, 45, "Фамилия");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.SafeText(txtFirstName.Text, 45, "Имя");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.SafeText(txtMiddleName.Text, 45, "Отчество");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Phone(txtPhone.Text, "Телефон", required: false);
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtEducation.Text, 45, "Образование");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbPosition.SelectedValue, "Должность");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.BirthDate(dpBirthDate.SelectedDate);
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

            var context = new MuseumDbContext();
            var employee = new Employee
            {
                LastName = txtLastName.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                MiddleName = txtMiddleName.Text.Trim(),
                IdPosition = (int)cmbPosition.SelectedValue!,
                Phone = TextHelper.TrimOrNull(txtPhone.Text),
                BirthDate = DateOnly.FromDateTime(dpBirthDate.SelectedDate!.Value),
                EducationLevel = TextHelper.TrimOrNull(txtEducation.Text)
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
