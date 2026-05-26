using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Countries;
public partial class EditCountryWindow : Window {
    private readonly int _id;
    public EditCountryWindow(Country selected) { InitializeComponent(); _id = selected.IdCountry; txtValue.Text = selected.CountryName; }
    private void btnSave_Click(object sender, RoutedEventArgs e) {
        try {
            var error = ValidationHelper.First(ValidationHelper.NotEmpty(txtValue.Text, "Название"), ValidationHelper.MaxLen(txtValue.Text, 255, "Название"));
            if (error != null) { MessageBox.Show(error); return; }
            using var context = new MuseumDbContext();
            var item = context.Countries.Find(_id); if (item == null) return;
            item.CountryName = txtValue.Text.Trim(); context.SaveChanges(); DialogResult = true; Close();
        } catch (Exception ex) { DbErrorHelper.Show(ex); }
    }
}
