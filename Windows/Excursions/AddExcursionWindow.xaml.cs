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
            DbErrorHelper.Show(ex);
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
            var duration = InputHelper.ParseInt(txtDuration.Text);
            var price = InputHelper.ParseDecimal(txtPrice.Text);
            var error = ValidationHelper.First(
                ValidationHelper.Combo(cmbExhibition.SelectedValue, "Выставка"),
                ValidationHelper.Combo(cmbGuide.SelectedValue, "Гид"),
                ValidationHelper.Combo(cmbCustomer.SelectedValue, "Заказчик"),
                ValidationHelper.Positive(duration, "Длительность"),
                ValidationHelper.NonNegative(price, "Цена"));
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var context = new MuseumDbContext();
            var excursion = new Excursion
            {
                IdExhibition = (int)cmbExhibition.SelectedValue!,
                IdGuide = (int)cmbGuide.SelectedValue!,
                IdCustomer = (int)cmbCustomer.SelectedValue!,
                Duration = duration,
                Price = price
            };
            context.Excursions.Add(excursion);
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
