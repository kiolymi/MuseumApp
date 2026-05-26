using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Links;
public partial class AddExhibitionHallLinkWindow : Window {
    public AddExhibitionHallLinkWindow() { InitializeComponent(); if (!LoadCombos()) Close(); }
    private bool LoadCombos() => ComboLoadHelper.TryLoadLinkCombos(cmb1, cmb2, "ExhibitionHall");
    private void btnAdd_Click(object sender, RoutedEventArgs e) {
        try {
            var error = ValidationHelper.First(ValidationHelper.Combo(cmb1.SelectedValue, "Выставка"), ValidationHelper.Combo(cmb2.SelectedValue, "Зал"));
            if (error != null) { MessageBox.Show(error); return; }
            using var context = new MuseumDbContext();
            context.ExhibitionHallLinks.Add(new ExhibitionHallLink { IdExhibition = (int)cmb1.SelectedValue!, IdHall = (int)cmb2.SelectedValue! });
            context.SaveChanges(); DialogResult = true; Close();
        } catch (Exception ex) { DbErrorHelper.Show(ex); }
    }
}
