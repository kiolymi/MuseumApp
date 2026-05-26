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
            var capacity = InputHelper.ParseNullableInt(txtCapacity.Text);
            var area = InputHelper.ParseNullableDouble(txtArea.Text);
            var error = ValidationHelper.First(
                ValidationHelper.NotEmpty(txtName.Text, "Название"),
                ValidationHelper.MaxLen(txtName.Text, 255, "Название"),
                ValidationHelper.Combo(cmbBranch.SelectedValue, "Филиал"),
                capacity.HasValue ? ValidationHelper.Positive(capacity.Value, "Вместимость") : null);
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var context = new MuseumDbContext();
            var hall = new Hall
            {
                HallName = txtName.Text.Trim(),
                IdBranch = (int)cmbBranch.SelectedValue!,
                Area = area,
                Capacity = capacity
            };
            context.Halls.Add(hall);
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
