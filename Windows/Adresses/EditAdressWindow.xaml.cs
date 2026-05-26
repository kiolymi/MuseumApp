using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Adresses;
public partial class EditAdressWindow : Window {
  private readonly int _id;
  public EditAdressWindow(Adress a) {
    InitializeComponent(); _id = a.IdAddress;
    txtCity.Text = a.City; txtStreet.Text = a.Street; txtHouse.Text = a.House; txtPostal.Text = a.PostalCode ?? "";
  }
  private void btnSave_Click(object sender, RoutedEventArgs e) {
    try {
      var err = ValidationHelper.First(
        ValidationHelper.NotEmpty(txtCity.Text, "Город"), ValidationHelper.NotEmpty(txtStreet.Text, "Улица"),
        ValidationHelper.NotEmpty(txtHouse.Text, "Дом"));
      if (err != null) { MessageBox.Show(err); return; }
      using var ctx = new MuseumDbContext();
      var item = ctx.Adresses.Find(_id); if (item == null) return;
      item.City = txtCity.Text.Trim(); item.Street = txtStreet.Text.Trim(); item.House = txtHouse.Text.Trim();
      item.PostalCode = string.IsNullOrWhiteSpace(txtPostal.Text) ? null : txtPostal.Text.Trim();
      ctx.SaveChanges(); DialogResult = true; Close();
    } catch (Exception ex) { DbErrorHelper.Show(ex); }
  }
}
