using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Storages;
public partial class AddStorageWindow : Window {
  public AddStorageWindow(){InitializeComponent(); if(!ComboLoadHelper.TryLoadBranchesForAdd(cmbBranch)) Close();}
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.NotEmpty(txtName.Text,"Название"),ValidationHelper.Combo(cmbBranch.SelectedValue,"Филиал"));
      if(err!=null){MessageBox.Show(err);return;}
      decimal? t=string.IsNullOrWhiteSpace(txtTemp.Text)?null:InputHelper.ParseDecimal(txtTemp.Text);
      decimal? h=string.IsNullOrWhiteSpace(txtHumidity.Text)?null:InputHelper.ParseDecimal(txtHumidity.Text);
      using var ctx=new MuseumDbContext();
      ctx.Storages.Add(new Storage{StorageName=txtName.Text.Trim(),IdBranch=(int)cmbBranch.SelectedValue!,Temperature=t,Humidity=h});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
