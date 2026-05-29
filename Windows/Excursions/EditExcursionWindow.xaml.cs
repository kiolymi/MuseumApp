using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Excursions;

public partial class EditExcursionWindow : Window
{
    private readonly int _id;

    public EditExcursionWindow(Excursion selected)
    {
        InitializeComponent();
        _id = selected.IdExcursion;
        txtDuration.Text = selected.Duration.ToString();
        txtPrice.Text = selected.Price.ToString();

        try
        {
            var context = new MuseumDbContext();
            cmbExhibition.ItemsSource = context.Exhibitions
                .Select(e => new { Id = e.IdExhibition, Name = e.ExhibitionName })
                .ToList();
            cmbExhibition.SelectedValue = selected.IdExhibition;
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
            Close();
            return;
        }

        ComboLoadHelper.LoadEmployees(cmbGuide, selected.IdGuide);
        ComboLoadHelper.LoadVisitors(cmbCustomer, selected.IdCustomer);
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
            var item = context.Excursions.Find(_id);
            if (item == null)
            {
                return;
            }

            item.IdExhibition = (int)cmbExhibition.SelectedValue!;
            item.IdGuide = (int)cmbGuide.SelectedValue!;
            item.IdCustomer = (int)cmbCustomer.SelectedValue!;
            item.Duration = duration;
            item.Price = price;

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
