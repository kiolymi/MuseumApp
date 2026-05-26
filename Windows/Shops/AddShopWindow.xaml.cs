using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Shops;
public partial class AddShopWindow : Window {
  public AddShopWindow(){InitializeComponent(); if(!ComboLoadHelper.TryLoadBranchesForAdd(cmbBranch)) Close();}
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbBranch.SelectedValue,"Филиал"),ValidationHelper.NotEmpty(txtName.Text,"Название"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      ctx.Shops.Add(new Shop{IdBranch=(int)cmbBranch.SelectedValue!,ShopName=txtName.Text.Trim(),Phone=N(txtPhone.Text),Email=N(txtEmail.Text),WorkingHours=N(txtHours.Text)});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
  static string? N(string s)=>string.IsNullOrWhiteSpace(s)?null:s.Trim();
}
