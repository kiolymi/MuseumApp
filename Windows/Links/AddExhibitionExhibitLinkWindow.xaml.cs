using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Links;
public partial class AddExhibitionExhibitLinkWindow : Window {
    public AddExhibitionExhibitLinkWindow() { InitializeComponent(); if (!LoadCombos()) Close(); }
    private bool LoadCombos() => ComboLoadHelper.TryLoadLinkCombos(cmb1, cmb2, "ExhibitionExhibit");
    private void btnAdd_Click(object sender, RoutedEventArgs e) {
        try {
            var error = ValidationHelper.First(ValidationHelper.Combo(cmb1.SelectedValue, "Выставка"), ValidationHelper.Combo(cmb2.SelectedValue, "Экспонат"));
            if (error != null) { MessageBox.Show(error); return; }
            using var context = new MuseumDbContext();
            context.ExhibitionExhibitLinks.Add(new ExhibitionExhibitLink { IdExhibition = (int)cmb1.SelectedValue!, IdExhibit = (int)cmb2.SelectedValue! });
            context.SaveChanges(); DialogResult = true; Close();
        } catch (Exception ex) { DbErrorHelper.Show(ex); }
    }
}
