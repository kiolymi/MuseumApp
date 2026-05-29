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
        {
            Close();
        }
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.Combo(cmbKeeper.SelectedValue, "Хранитель");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtDescription.Text, 255, "Описание");
        if (error != null)
        {
            return error;
        }

        return null;
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var error = ValidateForm();
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var context = new MuseumDbContext();
            var collection = new Collection
            {
                CollectionName = txtName.Text.Trim(),
                Description = TextHelper.TrimOrEmpty(txtDescription.Text),
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
