using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Positions;

public partial class EditPositionWindow : Window
{
    private readonly int _id;

    public EditPositionWindow(Position position)
    {
        InitializeComponent();
        _id = position.IdPosition;
        txtName.Text = position.PositionName;
        txtDesc.Text = position.Description ?? "";
        txtSalary.Text = position.Salary?.ToString() ?? "";
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtDesc.Text, 255, "Описание");
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

            decimal? salary = string.IsNullOrWhiteSpace(txtSalary.Text)
                ? null
                : InputHelper.ParseDecimal(txtSalary.Text);

            using var context = new MuseumDbContext();
            var item = context.Positions.Find(_id);
            if (item == null)
            {
                return;
            }

            item.PositionName = txtName.Text.Trim();
            item.Description = TextHelper.TrimOrNull(txtDesc.Text);
            item.Salary = salary;

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
