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
            var error = ValidationHelper.First(
                ValidationHelper.NotEmpty(txtName.Text, "Название"),
                ValidationHelper.MaxLen(txtName.Text, 255, "Название"),
                ValidationHelper.Combo(cmbKeeper.SelectedValue, "Хранитель"),
                ValidationHelper.MaxLen(txtDescription.Text, 255, "Описание"));
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var context = new MuseumDbContext();
            var collection = new Collection
            {
                CollectionName = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                IdKeeper = (int)cmbKeeper.SelectedValue!
            };
            context.Collections.Add(collection);
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
