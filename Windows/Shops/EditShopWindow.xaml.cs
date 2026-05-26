using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Shops;
public partial class EditShopWindow : Window {
  private readonly int _id;
  public EditShopWindow(Shop s){InitializeComponent();_id=s.IdShop;txtName.Text=s.ShopName;txtPhone.Text=s.Phone??"";txtEmail.Text=s.Email??"";txtHours.Text=s.WorkingHours??"";
    ComboLoadHelper.LoadBranches(cmbBranch,s.IdBranch);}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbBranch.SelectedValue,"Филиал"),ValidationHelper.NotEmpty(txtName.Text,"Название"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext(); var item=ctx.Shops.Find(_id); if(item==null)return;
      item.IdBranch=(int)cmbBranch.SelectedValue!; item.ShopName=txtName.Text.Trim(); item.Phone=N(txtPhone.Text); item.Email=N(txtEmail.Text); item.WorkingHours=N(txtHours.Text);
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
  static string? N(string s)=>string.IsNullOrWhiteSpace(s)?null:s.Trim();
}
