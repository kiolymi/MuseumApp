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
            var item = context.Employees.Find(_id);
            if (item == null) return;

            item.LastName = txtLastName.Text.Trim();
            item.FirstName = txtFirstName.Text.Trim();
            item.MiddleName = txtMiddleName.Text.Trim();
            item.IdPosition = (int)cmbPosition.SelectedValue!;
            item.Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();
            item.BirthDate = DateOnly.FromDateTime(dpBirthDate.SelectedDate!.Value);
            item.EducationLevel = string.IsNullOrWhiteSpace(txtEducation.Text) ? null : txtEducation.Text.Trim();

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
