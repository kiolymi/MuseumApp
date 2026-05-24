using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Collections;

public partial class AddCollectionWindow : Window
{
    public AddCollectionWindow()
    {
        InitializeComponent();
        var context = new MuseumDbContext();
        cmbKeeper.ItemsSource = context.Employees
            .Select(e => new { Id = e.IdEmployee, Name = $"{e.LastName} {e.FirstName} {e.MiddleName}" })
            .ToList();
        if (cmbKeeper.Items.Count > 0)
            cmbKeeper.SelectedIndex = 0;
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var collection = new Collection
            {
                CollectionName = txtName.Text,
                Description = txtDescription.Text,
                IdKeeper = (int)cmbKeeper.SelectedValue
            };
            context.Collections.Add(collection);
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
