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
            var context = new MuseumDbContext();
            var exhibition = new Exhibition
            {
                IdExhibition = (context.Exhibitions.Max(x => (int?)x.IdExhibition) ?? 0) + 1,
                ExhibitionName = txtName.Text,
                StartDate = dpStart.SelectedDate ?? DateTime.Today,
                EndDate = dpEnd.SelectedDate ?? DateTime.Today,
                Price = Convert.ToDecimal(txtPrice.Text),
                Theme = string.IsNullOrWhiteSpace(txtTheme.Text) ? null : txtTheme.Text,
                IdCurator = (int)cmbCurator.SelectedValue
            };
            context.Exhibitions.Add(exhibition);
            context.SaveChanges();
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
