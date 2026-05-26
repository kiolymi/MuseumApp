using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Links;
public partial class AddAuthorExLinkWindow : Window {
    public AddAuthorExLinkWindow() { InitializeComponent(); if (!LoadCombos()) Close(); }
    private bool LoadCombos() => ComboLoadHelper.TryLoadLinkCombos(cmb1, cmb2, "AuthorEx");
    private void btnAdd_Click(object sender, RoutedEventArgs e) {
        try {
            var error = ValidationHelper.First(ValidationHelper.Combo(cmb1.SelectedValue, "Автор"), ValidationHelper.Combo(cmb2.SelectedValue, "Экспонат"));
            if (error != null) { MessageBox.Show(error); return; }
            using var context = new MuseumDbContext();
            context.AuthorExLinks.Add(new AuthorExLink { IdAuthor = (int)cmb1.SelectedValue!, IdExh = (int)cmb2.SelectedValue! });
            context.SaveChanges(); DialogResult = true; Close();
        } catch (Exception ex) { DbErrorHelper.Show(ex); }
    }
}
