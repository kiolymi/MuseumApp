using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Collections;

public partial class EditCollectionWindow : Window
{
    private readonly int _id;

    public EditCollectionWindow(Collection selected)
    {
        InitializeComponent();
        _id = selected.IdCollection;
        txtName.Text = selected.CollectionName;
        txtDescription.Text = selected.Description;

        var context = new MuseumDbContext();
        cmbKeeper.ItemsSource = context.Employees
            .Select(e => new { Id = e.IdEmployee, Name = $"{e.LastName} {e.FirstName} {e.MiddleName}" })
            .ToList();
        cmbKeeper.SelectedValue = selected.IdKeeper;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var item = context.Collections.Find(_id);
            if (item == null) return;

            item.CollectionName = txtName.Text;
            item.Description = txtDescription.Text;
            item.IdKeeper = (int)cmbKeeper.SelectedValue;

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
