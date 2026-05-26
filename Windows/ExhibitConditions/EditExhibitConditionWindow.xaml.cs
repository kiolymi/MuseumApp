using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.ExhibitConditions;
public partial class EditExhibitConditionWindow : Window {
  private readonly int _id;
  public EditExhibitConditionWindow(ExhibitCondition c){InitializeComponent();_id=c.IdCondition;txtName.Text=c.ConditionName;txtDesc.Text=c.Description;}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.NotEmpty(txtName.Text,"Название"),ValidationHelper.NotEmpty(txtDesc.Text,"Описание"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext(); var item=ctx.ExhibitConditions.Find(_id); if(item==null)return;
      item.ConditionName=txtName.Text.Trim(); item.Description=txtDesc.Text.Trim(); ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
