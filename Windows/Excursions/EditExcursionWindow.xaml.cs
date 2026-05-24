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
            MessageBox.Show("Ошибка загрузки выставок: " + ex.Message);
            Close();
            return;
        }

        ComboLoadHelper.LoadEmployees(cmbGuide, selected.IdGuide);
        ComboLoadHelper.LoadVisitors(cmbCustomer, selected.IdCustomer);
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var item = context.Excursions.Find(_id);
            if (item == null) return;

            item.IdExhibition = (int)cmbExhibition.SelectedValue;
            item.IdGuide = (int)cmbGuide.SelectedValue;
            item.IdCustomer = (int)cmbCustomer.SelectedValue;
            item.Duration = InputHelper.ParseInt(txtDuration.Text);
            item.Price = InputHelper.ParseDecimal(txtPrice.Text);

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
