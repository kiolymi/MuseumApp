using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Exhibitions;

public partial class EditExhibitionWindow : Window
{
    private readonly int _id;

    public EditExhibitionWindow(Exhibition selected)
    {
        InitializeComponent();
        _id = selected.IdExhibition;
        txtName.Text = selected.ExhibitionName;
        dpStart.SelectedDate = selected.StartDate;
        dpEnd.SelectedDate = selected.EndDate;
        txtPrice.Text = selected.Price.ToString();
        txtTheme.Text = selected.Theme ?? "";

        ComboLoadHelper.LoadEmployees(cmbCurator, selected.IdCurator);
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var price = InputHelper.ParseDecimal(txtPrice.Text);
            var error = ValidationHelper.First(
                ValidationHelper.NotEmpty(txtName.Text, "Название"),
                ValidationHelper.MaxLen(txtName.Text, 255, "Название"),
                ValidationHelper.Dates(dpStart.SelectedDate, dpEnd.SelectedDate),
                ValidationHelper.Combo(cmbCurator.SelectedValue, "Куратор"),
                ValidationHelper.NonNegative(price, "Цена"),
                ValidationHelper.MaxLen(txtTheme.Text, 255, "Тема"));
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var context = new MuseumDbContext();
            var item = context.Exhibitions.Find(_id);
            if (item == null) return;

            item.ExhibitionName = txtName.Text.Trim();
            item.StartDate = dpStart.SelectedDate!.Value;
            item.EndDate = dpEnd.SelectedDate!.Value;
            item.Price = price;
            item.Theme = string.IsNullOrWhiteSpace(txtTheme.Text) ? null : txtTheme.Text.Trim();
            item.IdCurator = (int)cmbCurator.SelectedValue!;

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
