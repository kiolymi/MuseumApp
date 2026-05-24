using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Collections;

public partial class AddCollectionWindow : Window
{
    public AddCollectionWindow()
    {
        InitializeComponent();
        if (!ComboLoadHelper.TryLoadEmployeesForAdd(cmbKeeper))
            Close();
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
