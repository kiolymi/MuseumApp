using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Companies;
public partial class AddCompanyWindow : Window {
  public AddCompanyWindow()=>InitializeComponent();
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.NotEmpty(txtName.Text,"Название"); if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      ctx.Companies.Add(new Company{CompanyName=txtName.Text.Trim(),Inn=Null(txtInn.Text),LegalAddress=Null(txtAddress.Text),ContactPhone=Null(txtPhone.Text),ContactEmail=Null(txtEmail.Text)});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
  static string? Null(string s)=>string.IsNullOrWhiteSpace(s)?null:s.Trim();
}
