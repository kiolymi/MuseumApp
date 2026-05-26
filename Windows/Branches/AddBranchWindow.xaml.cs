using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Branches;
public partial class AddBranchWindow : Window {
  public AddBranchWindow(){InitializeComponent();
    if(!ComboLoadHelper.TryLoadMuseumsForAdd(cmbMuseum)||!ComboLoadHelper.TryLoadAdressesForAdd(cmbAddress)||!ComboLoadHelper.TryLoadEmployeesForAdd(cmbResponsible)) Close();}
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbMuseum.SelectedValue,"Музей"),ValidationHelper.NotEmpty(txtName.Text,"Название"),
        ValidationHelper.Combo(cmbAddress.SelectedValue,"Адрес"),ValidationHelper.Combo(cmbResponsible.SelectedValue,"Ответственный"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      ctx.Branches.Add(new Branch{IdMuseum=(int)cmbMuseum.SelectedValue!,BranchName=txtName.Text.Trim(),IdAddress=(int)cmbAddress.SelectedValue!,
        Phone=string.IsNullOrWhiteSpace(txtPhone.Text)?null:txtPhone.Text.Trim(),IdResponsible=(int)cmbResponsible.SelectedValue!});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
