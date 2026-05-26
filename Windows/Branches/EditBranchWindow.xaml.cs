using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Branches;
public partial class EditBranchWindow : Window {
  private readonly int _id;
  public EditBranchWindow(Branch b){InitializeComponent();_id=b.IdBranch;txtName.Text=b.BranchName;txtPhone.Text=b.Phone??"";
    ComboLoadHelper.LoadMuseums(cmbMuseum,b.IdMuseum); ComboLoadHelper.LoadAdresses(cmbAddress,b.IdAddress); ComboLoadHelper.LoadEmployees(cmbResponsible,b.IdResponsible);}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbMuseum.SelectedValue,"Музей"),ValidationHelper.NotEmpty(txtName.Text,"Название"),
        ValidationHelper.Combo(cmbAddress.SelectedValue,"Адрес"),ValidationHelper.Combo(cmbResponsible.SelectedValue,"Ответственный"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext(); var item=ctx.Branches.Find(_id); if(item==null)return;
      item.IdMuseum=(int)cmbMuseum.SelectedValue!; item.BranchName=txtName.Text.Trim(); item.IdAddress=(int)cmbAddress.SelectedValue!;
      item.Phone=string.IsNullOrWhiteSpace(txtPhone.Text)?null:txtPhone.Text.Trim(); item.IdResponsible=(int)cmbResponsible.SelectedValue!;
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
