using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.ExhibitMovements;

public partial class AddExhibitMovementWindow : Window
{
    public AddExhibitMovementWindow()
    {
        InitializeComponent();
        dpDate.SelectedDate = DateTime.Today;

        if (!ComboLoadHelper.TryLoadExhibitsForAdd(cmbExhibit)
            || !ComboLoadHelper.TryLoadEmployeesForAdd(cmbResponsible)
            || !ComboLoadHelper.TryLoadStoragesForAdd(cmbFrom)
            || !ComboLoadHelper.TryLoadReasonsForAdd(cmbReason))
        {
            Close();
            return;
        }

        ComboLoadHelper.TryLoadStoragesForAdd(cmbTo);
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.Combo(cmbExhibit.SelectedValue, "Экспонат");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbResponsible.SelectedValue, "Ответственный");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbFrom.SelectedValue, "Хранилище-источник");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbTo.SelectedValue, "Хранилище-назначение");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbReason.SelectedValue, "Причина");
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

            using var context = new MuseumDbContext();
            context.ExhibitMovements.Add(new ExhibitMovement
            {
                IdExhibit = (int)cmbExhibit.SelectedValue!,
                IdResponsible = (int)cmbResponsible.SelectedValue!,
                FromStorageId = (int)cmbFrom.SelectedValue!,
                ToStorageId = (int)cmbTo.SelectedValue!,
                MovementDate = dpDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(dpDate.SelectedDate.Value)
                    : DateOnly.FromDateTime(DateTime.Today),
                IdReason = (int)cmbReason.SelectedValue!
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
