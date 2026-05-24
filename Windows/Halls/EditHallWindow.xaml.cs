using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Halls;

public partial class EditHallWindow : Window
{
    private readonly int _id;

    public EditHallWindow(Hall selected)
    {
        InitializeComponent();
        _id = selected.IdHall;
        txtName.Text = selected.HallName;
        txtArea.Text = selected.Area?.ToString() ?? "";
        txtCapacity.Text = selected.Capacity?.ToString() ?? "";

        var context = new MuseumDbContext();
        cmbBranch.ItemsSource = context.Branches
            .Select(b => new { Id = b.IdBranch, Name = b.BranchName })
            .ToList();
        cmbBranch.SelectedValue = selected.IdBranch;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var item = context.Halls.Find(_id);
            if (item == null) return;

            item.HallName = txtName.Text;
            item.IdBranch = (int)cmbBranch.SelectedValue;
            item.Area = string.IsNullOrWhiteSpace(txtArea.Text) ? null : Convert.ToDouble(txtArea.Text);
            item.Capacity = string.IsNullOrWhiteSpace(txtCapacity.Text) ? null : Convert.ToInt32(txtCapacity.Text);

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
