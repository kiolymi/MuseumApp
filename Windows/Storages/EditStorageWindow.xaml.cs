using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Storages;
public partial class EditStorageWindow : Window {
  private readonly int _id;
  public EditStorageWindow(Storage s){InitializeComponent();_id=s.IdStorage;txtName.Text=s.StorageName;txtTemp.Text=s.Temperature?.ToString()??"";txtHumidity.Text=s.Humidity?.ToString()??"";
    ComboLoadHelper.LoadBranches(cmbBranch,s.IdBranch);}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.NotEmpty(txtName.Text,"Название"),ValidationHelper.Combo(cmbBranch.SelectedValue,"Филиал"));
      if(err!=null){MessageBox.Show(err);return;}
      decimal? t=string.IsNullOrWhiteSpace(txtTemp.Text)?null:InputHelper.ParseDecimal(txtTemp.Text);
      decimal? h=string.IsNullOrWhiteSpace(txtHumidity.Text)?null:InputHelper.ParseDecimal(txtHumidity.Text);
      using var ctx=new MuseumDbContext(); var item=ctx.Storages.Find(_id); if(item==null)return;
      item.StorageName=txtName.Text.Trim(); item.IdBranch=(int)cmbBranch.SelectedValue!; item.Temperature=t; item.Humidity=h;
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
