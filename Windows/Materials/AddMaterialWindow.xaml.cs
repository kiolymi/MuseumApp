using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Materials;
public partial class AddMaterialWindow : Window {
  public AddMaterialWindow() => InitializeComponent();
  private void btnAdd_Click(object sender, RoutedEventArgs e) {
    try {
      var err = ValidationHelper.NotEmpty(txtName.Text, "Название");
      if (err != null) { MessageBox.Show(err); return; }
      using var ctx = new MuseumDbContext();
      ctx.Materials.Add(new Material { MaterialName = txtName.Text.Trim(), Description = string.IsNullOrWhiteSpace(txtDesc.Text) ? null : txtDesc.Text.Trim() });
      ctx.SaveChanges(); DialogResult = true; Close();
    } catch (Exception ex) { DbErrorHelper.Show(ex); }
  }
}
