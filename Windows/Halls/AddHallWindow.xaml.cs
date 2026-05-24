using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Halls;

public partial class AddHallWindow : Window
{
    public AddHallWindow()
    {
        InitializeComponent();
        if (!ComboLoadHelper.TryLoadBranchesForAdd(cmbBranch))
            Close();
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var hall = new Hall
            {
                HallName = txtName.Text,
                IdBranch = (int)cmbBranch.SelectedValue,
                Area = string.IsNullOrWhiteSpace(txtArea.Text) ? null : Convert.ToDouble(txtArea.Text),
                Capacity = string.IsNullOrWhiteSpace(txtCapacity.Text) ? null : Convert.ToInt32(txtCapacity.Text)
            };
            context.Halls.Add(hall);
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
