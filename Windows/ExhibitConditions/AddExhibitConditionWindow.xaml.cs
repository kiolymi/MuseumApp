using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.ExhibitConditions;
public partial class AddExhibitConditionWindow : Window {
  public AddExhibitConditionWindow() => InitializeComponent();
  private void btnAdd_Click(object sender, RoutedEventArgs e) {
    try {
      var err = ValidationHelper.First(ValidationHelper.NotEmpty(txtName.Text,"Название"), ValidationHelper.NotEmpty(txtDesc.Text,"Описание"));
      if (err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      ctx.ExhibitConditions.Add(new ExhibitCondition{ConditionName=txtName.Text.Trim(),Description=txtDesc.Text.Trim()});
      ctx.SaveChanges(); DialogResult=true; Close();
    } catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
