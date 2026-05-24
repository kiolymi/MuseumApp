using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Excursions;

public partial class AddExcursionWindow : Window
{
    public AddExcursionWindow()
    {
        InitializeComponent();

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

        if (cmbExhibition.Items.Count > 0) cmbExhibition.SelectedIndex = 0;
        if (cmbGuide.Items.Count > 0) cmbGuide.SelectedIndex = 0;
        if (cmbCustomer.Items.Count > 0) cmbCustomer.SelectedIndex = 0;
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
                Duration = Convert.ToInt32(txtDuration.Text),
                Price = Convert.ToDecimal(txtPrice.Text)
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
