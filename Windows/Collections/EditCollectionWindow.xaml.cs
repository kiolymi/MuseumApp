using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

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

        ComboLoadHelper.LoadEmployees(cmbKeeper, selected.IdKeeper);
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
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
            var item = context.Collections.Find(_id);
            if (item == null) return;

            item.CollectionName = txtName.Text.Trim();
            item.Description = txtDescription.Text.Trim();
            item.IdKeeper = (int)cmbKeeper.SelectedValue!;

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
