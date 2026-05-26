using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Materials;
public partial class EditMaterialWindow : Window {
  private readonly int _id;
  public EditMaterialWindow(Material m) { InitializeComponent(); _id = m.IdMaterial; txtName.Text = m.MaterialName; txtDesc.Text = m.Description ?? ""; }
  private void btnSave_Click(object sender, RoutedEventArgs e) {
    try {
      var err = ValidationHelper.NotEmpty(txtName.Text, "Название");
      if (err != null) { MessageBox.Show(err); return; }
      using var ctx = new MuseumDbContext();
      var item = ctx.Materials.Find(_id); if (item == null) return;
      item.MaterialName = txtName.Text.Trim(); item.Description = string.IsNullOrWhiteSpace(txtDesc.Text) ? null : txtDesc.Text.Trim();
      ctx.SaveChanges(); DialogResult = true; Close();
    } catch (Exception ex) { DbErrorHelper.Show(ex); }
  }
}
