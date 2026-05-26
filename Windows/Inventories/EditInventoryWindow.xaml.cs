using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Inventories;
public partial class EditInventoryWindow : Window {
  private readonly int _shop; private readonly int _product;
  public EditInventoryWindow(Inventory i){InitializeComponent();_shop=i.IdShop;_product=i.IdProduct;txtQty.Text=i.Quantity.ToString();
    ComboLoadHelper.LoadShops(cmbShop,i.IdShop); ComboLoadHelper.LoadProducts(cmbProduct,i.IdProduct);}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var qty=InputHelper.ParseInt(txtQty.Text);
      var err=ValidationHelper.NonNegative(qty,"Количество"); if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      var item=ctx.Inventories.Find(_shop,_product); if(item==null)return;
      item.Quantity=qty; ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
