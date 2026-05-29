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
            if (cmbExhibition.Items.Count > 0)
            {
                cmbExhibition.SelectedIndex = 0;
            }
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
        {
            Close();
        }
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.Combo(cmbExhibition.SelectedValue, "Выставка");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbGuide.SelectedValue, "Гид");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbCustomer.SelectedValue, "Заказчик");
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

            var duration = InputHelper.ParseInt(txtDuration.Text);
            var durationError = ValidationHelper.Positive(duration, "Длительность");
            if (durationError != null)
            {
                MessageBox.Show(durationError);
                return;
            }

            var price = InputHelper.ParseDecimal(txtPrice.Text);
            var priceError = ValidationHelper.NonNegative(price, "Цена");
            if (priceError != null)
            {
                MessageBox.Show(priceError);
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
