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

            var context = new MuseumDbContext();
            var item = context.Employees.Find(_id);
            if (item == null)
            {
                return;
            }

            item.LastName = txtLastName.Text.Trim();
            item.FirstName = txtFirstName.Text.Trim();
            item.MiddleName = txtMiddleName.Text.Trim();
            item.IdPosition = (int)cmbPosition.SelectedValue!;
            item.Phone = TextHelper.TrimOrNull(txtPhone.Text);
            item.BirthDate = DateOnly.FromDateTime(dpBirthDate.SelectedDate!.Value);
            item.EducationLevel = TextHelper.TrimOrNull(txtEducation.Text);

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
