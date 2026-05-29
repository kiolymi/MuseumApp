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

    private void btnSave_Click(object sender, RoutedEventArgs e)
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
            var item = context.Collections.Find(_id);
            if (item == null)
            {
                return;
            }

            item.CollectionName = txtName.Text.Trim();
            item.Description = TextHelper.TrimOrEmpty(txtDescription.Text);
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
