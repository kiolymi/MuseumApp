using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Positions;
public partial class EditPositionWindow : Window {
  private readonly int _id;
  public EditPositionWindow(Position p) { InitializeComponent(); _id=p.IdPosition; txtName.Text=p.PositionName; txtDesc.Text=p.Description??""; txtSalary.Text=p.Salary?.ToString()??""; }
  private void btnSave_Click(object sender, RoutedEventArgs e) {
    try {
      var err = ValidationHelper.NotEmpty(txtName.Text, "Название");
      if (err != null) { MessageBox.Show(err); return; }
      decimal? salary = string.IsNullOrWhiteSpace(txtSalary.Text) ? null : InputHelper.ParseDecimal(txtSalary.Text);
      using var ctx = new MuseumDbContext();
      var item = ctx.Positions.Find(_id); if (item==null) return;
      item.PositionName=txtName.Text.Trim(); item.Description=string.IsNullOrWhiteSpace(txtDesc.Text)?null:txtDesc.Text.Trim(); item.Salary=salary;
      ctx.SaveChanges(); DialogResult=true; Close();
    } catch (Exception ex) { DbErrorHelper.Show(ex); }
  }
}
