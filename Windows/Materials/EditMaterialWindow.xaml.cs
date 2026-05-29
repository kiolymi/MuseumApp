using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Materials;

public partial class EditMaterialWindow : Window
{
    private readonly int _id;

    public EditMaterialWindow(Material material)
    {
        InitializeComponent();
        _id = material.IdMaterial;
        txtName.Text = material.MaterialName;
        txtDesc.Text = material.Description ?? "";
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtDesc.Text, 255, "Описание");
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

            using var context = new MuseumDbContext();
            var item = context.Materials.Find(_id);
            if (item == null)
            {
                return;
            }

            item.MaterialName = txtName.Text.Trim();
            item.Description = TextHelper.TrimOrNull(txtDesc.Text);

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
