using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.ExhibitMovements;
public partial class AddExhibitMovementWindow : Window {
  public AddExhibitMovementWindow(){InitializeComponent(); dpDate.SelectedDate=DateTime.Today;
    if(!ComboLoadHelper.TryLoadExhibitsForAdd(cmbExhibit)||!ComboLoadHelper.TryLoadEmployeesForAdd(cmbResponsible)||
       !ComboLoadHelper.TryLoadStoragesForAdd(cmbFrom)||!ComboLoadHelper.TryLoadReasonsForAdd(cmbReason)) Close();
    ComboLoadHelper.TryLoadStoragesForAdd(cmbTo);}
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbExhibit.SelectedValue,"Экспонат"),ValidationHelper.Combo(cmbResponsible.SelectedValue,"Ответственный"),
        ValidationHelper.Combo(cmbFrom.SelectedValue,"Хранилище-источник"),ValidationHelper.Combo(cmbTo.SelectedValue,"Хранилище-назначение"),ValidationHelper.Combo(cmbReason.SelectedValue,"Причина"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      ctx.ExhibitMovements.Add(new ExhibitMovement{
        IdExhibit=(int)cmbExhibit.SelectedValue!,IdResponsible=(int)cmbResponsible.SelectedValue!,
        FromStorageId=(int)cmbFrom.SelectedValue!,ToStorageId=(int)cmbTo.SelectedValue!,
        MovementDate=dpDate.SelectedDate.HasValue?DateOnly.FromDateTime(dpDate.SelectedDate.Value):DateOnly.FromDateTime(DateTime.Today),
        IdReason=(int)cmbReason.SelectedValue!});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
