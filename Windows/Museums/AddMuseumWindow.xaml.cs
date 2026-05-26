using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Museums;
public partial class AddMuseumWindow : Window {
  public AddMuseumWindow(){InitializeComponent();
    if(!ComboLoadHelper.TryLoadEmployeesForAdd(cmbDirector)||!ComboLoadHelper.TryLoadAdressesForAdd(cmbAddress)) Close();}
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.NotEmpty(txtName.Text,"Название"),ValidationHelper.Combo(cmbDirector.SelectedValue,"Директор"),ValidationHelper.Combo(cmbAddress.SelectedValue,"Адрес"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      ctx.Museums.Add(new Museum{Name=txtName.Text.Trim(),IdDirector=(int)cmbDirector.SelectedValue!,IdAddress=(int)cmbAddress.SelectedValue!});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
