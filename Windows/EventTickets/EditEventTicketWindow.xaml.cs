using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.EventTickets;
public partial class EditEventTicketWindow : Window {
  private readonly int _visitor; private readonly int _event;
  public EditEventTicketWindow(EventTicket t){InitializeComponent();_visitor=t.IdVisitor;_event=t.IdEvent;txtPrice.Text=t.ActualPrice.ToString();
    dpPurchase.SelectedDate=t.PurchaseDate.ToDateTime(TimeOnly.MinValue);
    ComboLoadHelper.LoadVisitors(cmbVisitor,t.IdVisitor); ComboLoadHelper.LoadEvents(cmbEvent,t.IdEvent);}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var price=InputHelper.ParseDecimal(txtPrice.Text);
      var err=ValidationHelper.NonNegative(price,"Цена"); if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext(); var item=ctx.EventTickets.Find(_visitor,_event); if(item==null)return;
      item.PurchaseDate=DateOnly.FromDateTime(dpPurchase.SelectedDate??DateTime.Today); item.ActualPrice=price;
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
