using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

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

        var context = new MuseumDbContext();
        cmbCurator.ItemsSource = context.Employees
            .Select(e => new { Id = e.IdEmployee, Name = $"{e.LastName} {e.FirstName} {e.MiddleName}" })
            .ToList();
        cmbCurator.SelectedValue = selected.IdCurator;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var item = context.Exhibitions.Find(_id);
            if (item == null) return;

            item.ExhibitionName = txtName.Text;
            item.StartDate = dpStart.SelectedDate ?? item.StartDate;
            item.EndDate = dpEnd.SelectedDate ?? item.EndDate;
            item.Price = Convert.ToDecimal(txtPrice.Text);
            item.Theme = string.IsNullOrWhiteSpace(txtTheme.Text) ? null : txtTheme.Text;
            item.IdCurator = (int)cmbCurator.SelectedValue;

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
