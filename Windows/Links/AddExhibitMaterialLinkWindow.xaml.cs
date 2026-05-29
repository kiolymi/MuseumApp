using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Links;

public partial class AddExhibitMaterialLinkWindow : Window
{
    public AddExhibitMaterialLinkWindow()
    {
        InitializeComponent();
        if (!LoadCombos())
        {
            Close();
        }
    }

    private bool LoadCombos()
    {
        return ComboLoadHelper.TryLoadLinkCombos(cmb1, cmb2, "ExhibitMaterial");
    }

    private string? ValidateForm()
    {
        var error = ValidationHelper.Combo(cmb1.SelectedValue, "Экспонат");
        if (error != null)
        {
            return error;
        }

        return ValidationHelper.Combo(cmb2.SelectedValue, "Материал");
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

            using var context = new MuseumDbContext();
            context.ExhibitMaterialLinks.Add(new ExhibitMaterialLink
            {
                IdExhibit = (int)cmb1.SelectedValue!,
                IdMaterial = (int)cmb2.SelectedValue!
            });
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
