using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Positions;

public partial class AddPositionWindow : Window
{
    public AddPositionWindow()
    {
        InitializeComponent();
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

            decimal? salary = string.IsNullOrWhiteSpace(txtSalary.Text)
                ? null
                : InputHelper.ParseDecimal(txtSalary.Text);

            using var context = new MuseumDbContext();
            context.Positions.Add(new Position
            {
                PositionName = txtName.Text.Trim(),
                Description = TextHelper.TrimOrNull(txtDesc.Text),
                Salary = salary
            });
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
