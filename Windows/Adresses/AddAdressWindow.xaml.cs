using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Adresses;
public partial class AddAdressWindow : Window {
  public AddAdressWindow() => InitializeComponent();
  private void btnAdd_Click(object sender, RoutedEventArgs e) {
    try {
      var err = ValidationHelper.First(
        ValidationHelper.NotEmpty(txtCity.Text, "Город"), ValidationHelper.MaxLen(txtCity.Text, 45, "Город"),
        ValidationHelper.NotEmpty(txtStreet.Text, "Улица"), ValidationHelper.MaxLen(txtStreet.Text, 45, "Улица"),
        ValidationHelper.NotEmpty(txtHouse.Text, "Дом"), ValidationHelper.MaxLen(txtHouse.Text, 45, "Дом"));
      if (err != null) { MessageBox.Show(err); return; }
      using var ctx = new MuseumDbContext();
      ctx.Adresses.Add(new Adress {
        City = txtCity.Text.Trim(), Street = txtStreet.Text.Trim(), House = txtHouse.Text.Trim(),
        PostalCode = string.IsNullOrWhiteSpace(txtPostal.Text) ? null : txtPostal.Text.Trim()
      });
      ctx.SaveChanges(); DialogResult = true; Close();
    } catch (Exception ex) { DbErrorHelper.Show(ex); }
  }
}
