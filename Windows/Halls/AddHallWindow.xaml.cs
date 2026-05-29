using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Halls;

public partial class AddHallWindow : Window
{
    public AddHallWindow()
    {
        InitializeComponent();
        if (!ComboLoadHelper.TryLoadBranchesForAdd(cmbBranch))
        {
            Close();
        }
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbBranch.SelectedValue, "Филиал");
        if (error != null)
        {
            return error;
        }

        var capacity = InputHelper.ParseNullableInt(txtCapacity.Text);
        if (capacity.HasValue)
        {
            error = ValidationHelper.Positive(capacity.Value, "Вместимость");
            if (error != null)
            {
                return error;
            }
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

            var capacity = InputHelper.ParseNullableInt(txtCapacity.Text);
            var area = InputHelper.ParseNullableDouble(txtArea.Text);

            var context = new MuseumDbContext();
            var hall = new Hall
            {
                HallName = txtName.Text.Trim(),
                IdBranch = (int)cmbBranch.SelectedValue!,
                Area = area,
                Capacity = capacity
            };
            context.Halls.Add(hall);
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
