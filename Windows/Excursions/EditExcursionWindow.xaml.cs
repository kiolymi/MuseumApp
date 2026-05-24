using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

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

        var context = new MuseumDbContext();
        cmbExhibition.ItemsSource = context.Exhibitions
            .Select(e => new { Id = e.IdExhibition, Name = e.ExhibitionName })
            .ToList();
        cmbGuide.ItemsSource = context.Employees
            .Select(e => new { Id = e.IdEmployee, Name = $"{e.LastName} {e.FirstName}" })
            .ToList();
        cmbCustomer.ItemsSource = context.Visitors
            .Select(v => new { Id = v.IdVisitor, Name = $"{v.LastName} {v.FirstName}" })
            .ToList();

        cmbExhibition.SelectedValue = selected.IdExhibition;
        cmbGuide.SelectedValue = selected.IdGuide;
        cmbCustomer.SelectedValue = selected.IdCustomer;
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
            item.Duration = Convert.ToInt32(txtDuration.Text);
            item.Price = Convert.ToDecimal(txtPrice.Text);

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
