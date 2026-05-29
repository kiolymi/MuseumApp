using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.ExhibitMovements;

public partial class EditExhibitMovementWindow : Window
{
    private readonly int _id;

    public EditExhibitMovementWindow(ExhibitMovement movement)
    {
        InitializeComponent();
        _id = movement.IdMovement;

        ComboLoadHelper.LoadExhibits(cmbExhibit, movement.IdExhibit);
        ComboLoadHelper.LoadEmployees(cmbResponsible, movement.IdResponsible);
        ComboLoadHelper.LoadStorages(cmbFrom, movement.FromStorageId);
        ComboLoadHelper.LoadStorages(cmbTo, movement.ToStorageId);
        ComboLoadHelper.LoadReasons(cmbReason, movement.IdReason);

        if (movement.MovementDate.HasValue)
        {
            dpDate.SelectedDate = movement.MovementDate.Value.ToDateTime(TimeOnly.MinValue);
        }
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

        error = ValidationHelper.Combo(cmbFrom.SelectedValue, "Из");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbTo.SelectedValue, "В");
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

            using var context = new MuseumDbContext();
            var item = context.ExhibitMovements.Find(_id);
            if (item == null)
            {
                return;
            }

            item.IdExhibit = (int)cmbExhibit.SelectedValue!;
            item.IdResponsible = (int)cmbResponsible.SelectedValue!;
            item.FromStorageId = (int)cmbFrom.SelectedValue!;
            item.ToStorageId = (int)cmbTo.SelectedValue!;
            item.IdReason = (int)cmbReason.SelectedValue!;
            item.MovementDate = dpDate.SelectedDate.HasValue
                ? DateOnly.FromDateTime(dpDate.SelectedDate.Value)
                : null;

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
