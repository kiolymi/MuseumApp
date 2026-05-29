using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Links;

public partial class AddAuthorExLinkWindow : Window
{
    public AddAuthorExLinkWindow()
    {
        InitializeComponent();
        if (!LoadCombos())
        {
            Close();
        }
    }

    private bool LoadCombos()
    {
        return ComboLoadHelper.TryLoadLinkCombos(cmb1, cmb2, "AuthorEx");
    }

    private string? ValidateForm()
    {
        var error = ValidationHelper.Combo(cmb1.SelectedValue, "Автор");
        if (error != null)
        {
            return error;
        }

        return ValidationHelper.Combo(cmb2.SelectedValue, "Экспонат");
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
            context.AuthorExLinks.Add(new AuthorExLink
            {
                IdAuthor = (int)cmb1.SelectedValue!,
                IdExh = (int)cmb2.SelectedValue!
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
