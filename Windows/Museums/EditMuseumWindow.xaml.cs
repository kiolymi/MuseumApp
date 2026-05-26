using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Museums;
public partial class EditMuseumWindow : Window {
  private readonly int _id;
  public EditMuseumWindow(Museum m){InitializeComponent();_id=m.IdMuseum;txtName.Text=m.Name;
    ComboLoadHelper.LoadEmployees(cmbDirector,m.IdDirector); ComboLoadHelper.LoadAdresses(cmbAddress,m.IdAddress);}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.NotEmpty(txtName.Text,"Название"),ValidationHelper.Combo(cmbDirector.SelectedValue,"Директор"),ValidationHelper.Combo(cmbAddress.SelectedValue,"Адрес"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext(); var item=ctx.Museums.Find(_id); if(item==null)return;
      item.Name=txtName.Text.Trim(); item.IdDirector=(int)cmbDirector.SelectedValue!; item.IdAddress=(int)cmbAddress.SelectedValue!;
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
