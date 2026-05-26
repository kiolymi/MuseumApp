using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Restorations;
public partial class AddRestorationWindow : Window {
  public AddRestorationWindow(){InitializeComponent(); dpStart.SelectedDate=DateTime.Today;
    if(!ComboLoadHelper.TryLoadExhibitsForAdd(cmbExhibit)||!ComboLoadHelper.TryLoadEmployeesForAdd(cmbRestorer)) Close();}
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbExhibit.SelectedValue,"Экспонат"),ValidationHelper.Combo(cmbRestorer.SelectedValue,"Реставратор"));
      if(err!=null){MessageBox.Show(err);return;}
      decimal? cost=string.IsNullOrWhiteSpace(txtCost.Text)?null:InputHelper.ParseDecimal(txtCost.Text);
      using var ctx=new MuseumDbContext();
      ctx.Restorations.Add(new Restoration{IdExhibit=(int)cmbExhibit.SelectedValue!,IdRestorer=(int)cmbRestorer.SelectedValue!,
        StartDate=dpStart.SelectedDate??DateTime.Today, EndDate=dpEnd.SelectedDate, Cost=cost, WorkDescription=string.IsNullOrWhiteSpace(txtDesc.Text)?null:txtDesc.Text.Trim()});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
