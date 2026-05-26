using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.EventTickets;
public partial class AddEventTicketWindow : Window {
  public AddEventTicketWindow(){InitializeComponent(); dpPurchase.SelectedDate=DateTime.Today;
    if(!ComboLoadHelper.TryLoadVisitorsForAdd(cmbVisitor)||!ComboLoadHelper.TryLoadEventsForAdd(cmbEvent)) Close();}
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var price=InputHelper.ParseDecimal(txtPrice.Text);
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbVisitor.SelectedValue,"Посетитель"),ValidationHelper.Combo(cmbEvent.SelectedValue,"Мероприятие"),ValidationHelper.NonNegative(price,"Цена"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      ctx.EventTickets.Add(new EventTicket{IdVisitor=(int)cmbVisitor.SelectedValue!,IdEvent=(int)cmbEvent.SelectedValue!,
        PurchaseDate=DateOnly.FromDateTime(dpPurchase.SelectedDate??DateTime.Today),ActualPrice=price});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
