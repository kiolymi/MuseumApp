using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Companies;
public partial class EditCompanyWindow : Window {
  private readonly int _id;
  public EditCompanyWindow(Company c){InitializeComponent();_id=c.IdCompany;txtName.Text=c.CompanyName;txtInn.Text=c.Inn??"";txtAddress.Text=c.LegalAddress??"";txtPhone.Text=c.ContactPhone??"";txtEmail.Text=c.ContactEmail??"";}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.NotEmpty(txtName.Text,"Название"); if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext(); var item=ctx.Companies.Find(_id); if(item==null)return;
      item.CompanyName=txtName.Text.Trim(); item.Inn=Null(txtInn.Text); item.LegalAddress=Null(txtAddress.Text); item.ContactPhone=Null(txtPhone.Text); item.ContactEmail=Null(txtEmail.Text);
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
  static string? Null(string s)=>string.IsNullOrWhiteSpace(s)?null:s.Trim();
}
