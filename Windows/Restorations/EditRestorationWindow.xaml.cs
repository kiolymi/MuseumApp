using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Restorations;

public partial class EditRestorationWindow : Window
{
    private readonly int _id;

    public EditRestorationWindow(Restoration restoration)
    {
        InitializeComponent();
        _id = restoration.IdRestoration;
        txtCost.Text = restoration.Cost?.ToString() ?? "";
        txtDesc.Text = restoration.WorkDescription ?? "";
        dpStart.SelectedDate = restoration.StartDate;
        dpEnd.SelectedDate = restoration.EndDate;

        ComboLoadHelper.LoadExhibits(cmbExhibit, restoration.IdExhibit);
        ComboLoadHelper.LoadEmployees(cmbRestorer, restoration.IdRestorer);
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.Combo(cmbExhibit.SelectedValue, "Экспонат");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbRestorer.SelectedValue, "Реставратор");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtDesc.Text, 255, "Описание работ");
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

            decimal? cost = string.IsNullOrWhiteSpace(txtCost.Text)
                ? null
                : InputHelper.ParseDecimal(txtCost.Text);

            using var context = new MuseumDbContext();
            var item = context.Restorations.Find(_id);
            if (item == null)
            {
                return;
            }

            item.IdExhibit = (int)cmbExhibit.SelectedValue!;
            item.IdRestorer = (int)cmbRestorer.SelectedValue!;
            item.StartDate = dpStart.SelectedDate ?? item.StartDate;
            item.EndDate = dpEnd.SelectedDate;
            item.Cost = cost;
            item.WorkDescription = TextHelper.TrimOrNull(txtDesc.Text);

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
