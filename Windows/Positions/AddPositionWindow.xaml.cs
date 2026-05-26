using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Positions;
public partial class AddPositionWindow : Window {
  public AddPositionWindow() => InitializeComponent();
  private void btnAdd_Click(object sender, RoutedEventArgs e) {
    try {
      var err = ValidationHelper.NotEmpty(txtName.Text, "Название");
      if (err != null) { MessageBox.Show(err); return; }
      decimal? salary = string.IsNullOrWhiteSpace(txtSalary.Text) ? null : InputHelper.ParseDecimal(txtSalary.Text);
      using var ctx = new MuseumDbContext();
      ctx.Positions.Add(new Position { PositionName = txtName.Text.Trim(), Description = string.IsNullOrWhiteSpace(txtDesc.Text)?null:txtDesc.Text.Trim(), Salary = salary });
      ctx.SaveChanges(); DialogResult=true; Close();
    } catch (Exception ex) { DbErrorHelper.Show(ex); }
  }
}
