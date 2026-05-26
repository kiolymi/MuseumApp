using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Exhibitions;

public partial class AddExhibitionWindow : Window
{
    public AddExhibitionWindow()
    {
        InitializeComponent();
        dpStart.SelectedDate = DateTime.Today;
        dpEnd.SelectedDate = DateTime.Today.AddDays(7);

        if (!ComboLoadHelper.TryLoadEmployeesForAdd(cmbCurator))
            Close();
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
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
            var exhibition = new Exhibition
            {
                IdExhibition = (context.Exhibitions.Max(x => (int?)x.IdExhibition) ?? 0) + 1,
                ExhibitionName = txtName.Text.Trim(),
                StartDate = dpStart.SelectedDate!.Value,
                EndDate = dpEnd.SelectedDate!.Value,
                Price = price,
                Theme = string.IsNullOrWhiteSpace(txtTheme.Text) ? null : txtTheme.Text.Trim(),
                IdCurator = (int)cmbCurator.SelectedValue!
            };
            context.Exhibitions.Add(exhibition);
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
