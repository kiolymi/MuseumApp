using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Halls;

public partial class EditHallWindow : Window
{
    private readonly int _id;

    public EditHallWindow(Hall selected)
    {
        InitializeComponent();
        _id = selected.IdHall;
        txtName.Text = selected.HallName;
        txtArea.Text = selected.Area?.ToString() ?? "";
        txtCapacity.Text = selected.Capacity?.ToString() ?? "";

        ComboLoadHelper.LoadBranches(cmbBranch, selected.IdBranch);
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

            var capacity = InputHelper.ParseNullableInt(txtCapacity.Text);
            var area = InputHelper.ParseNullableDouble(txtArea.Text);

            var context = new MuseumDbContext();
            var item = context.Halls.Find(_id);
            if (item == null)
            {
                return;
            }

            item.HallName = txtName.Text.Trim();
            item.IdBranch = (int)cmbBranch.SelectedValue!;
            item.Area = area;
            item.Capacity = capacity;

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
