using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.ExhibitMovements;
public partial class EditExhibitMovementWindow : Window {
  private readonly int _id;
  public EditExhibitMovementWindow(ExhibitMovement m){InitializeComponent();_id=m.IdMovement;
    ComboLoadHelper.LoadExhibits(cmbExhibit,m.IdExhibit); ComboLoadHelper.LoadEmployees(cmbResponsible,m.IdResponsible);
    ComboLoadHelper.LoadStorages(cmbFrom,m.FromStorageId); ComboLoadHelper.LoadStorages(cmbTo,m.ToStorageId);
    ComboLoadHelper.LoadReasons(cmbReason,m.IdReason);
    if(m.MovementDate.HasValue) dpDate.SelectedDate=m.MovementDate.Value.ToDateTime(TimeOnly.MinValue);}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbExhibit.SelectedValue,"Экспонат"),ValidationHelper.Combo(cmbResponsible.SelectedValue,"Ответственный"),
        ValidationHelper.Combo(cmbFrom.SelectedValue,"Из"),ValidationHelper.Combo(cmbTo.SelectedValue,"В"),ValidationHelper.Combo(cmbReason.SelectedValue,"Причина"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext(); var item=ctx.ExhibitMovements.Find(_id); if(item==null)return;
      item.IdExhibit=(int)cmbExhibit.SelectedValue!; item.IdResponsible=(int)cmbResponsible.SelectedValue!;
      item.FromStorageId=(int)cmbFrom.SelectedValue!; item.ToStorageId=(int)cmbTo.SelectedValue!;
      item.IdReason=(int)cmbReason.SelectedValue!;
      item.MovementDate=dpDate.SelectedDate.HasValue?DateOnly.FromDateTime(dpDate.SelectedDate.Value):null;
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
