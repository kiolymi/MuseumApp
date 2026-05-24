using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Excursions;

public partial class AddExcursionWindow : Window
{
    public AddExcursionWindow()
    {
        InitializeComponent();

        try
        {
            var context = new MuseumDbContext();
            cmbExhibition.ItemsSource = context.Exhibitions
                .Select(e => new { Id = e.IdExhibition, Name = e.ExhibitionName })
                .ToList();
            if (cmbExhibition.Items.Count > 0) cmbExhibition.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка загрузки выставок: " + ex.Message);
            Close();
            return;
        }

        if (!ComboLoadHelper.TryLoadEmployeesForAdd(cmbGuide))
        {
            Close();
            return;
        }

        if (!ComboLoadHelper.TryLoadVisitorsForAdd(cmbCustomer))
            Close();
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var excursion = new Excursion
            {
                IdExhibition = (int)cmbExhibition.SelectedValue,
                IdGuide = (int)cmbGuide.SelectedValue,
                IdCustomer = (int)cmbCustomer.SelectedValue,
                Duration = InputHelper.ParseInt(txtDuration.Text),
                Price = InputHelper.ParseDecimal(txtPrice.Text)
            };
            context.Excursions.Add(excursion);
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
