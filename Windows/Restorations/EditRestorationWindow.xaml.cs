using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Restorations;
public partial class EditRestorationWindow : Window {
  private readonly int _id;
  public EditRestorationWindow(Restoration r){InitializeComponent();_id=r.IdRestoration;txtCost.Text=r.Cost?.ToString()??"";txtDesc.Text=r.WorkDescription??"";
    dpStart.SelectedDate=r.StartDate; dpEnd.SelectedDate=r.EndDate;
    ComboLoadHelper.LoadExhibits(cmbExhibit,r.IdExhibit); ComboLoadHelper.LoadEmployees(cmbRestorer,r.IdRestorer);}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbExhibit.SelectedValue,"Экспонат"),ValidationHelper.Combo(cmbRestorer.SelectedValue,"Реставратор"));
      if(err!=null){MessageBox.Show(err);return;}
      decimal? cost=string.IsNullOrWhiteSpace(txtCost.Text)?null:InputHelper.ParseDecimal(txtCost.Text);
      using var ctx=new MuseumDbContext(); var item=ctx.Restorations.Find(_id); if(item==null)return;
      item.IdExhibit=(int)cmbExhibit.SelectedValue!; item.IdRestorer=(int)cmbRestorer.SelectedValue!;
      item.StartDate=dpStart.SelectedDate??item.StartDate; item.EndDate=dpEnd.SelectedDate; item.Cost=cost; item.WorkDescription=string.IsNullOrWhiteSpace(txtDesc.Text)?null:txtDesc.Text.Trim();
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
