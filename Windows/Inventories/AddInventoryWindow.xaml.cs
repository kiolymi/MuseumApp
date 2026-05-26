using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Inventories;
public partial class AddInventoryWindow : Window {
  public AddInventoryWindow(){InitializeComponent();
    if(!ComboLoadHelper.TryLoadShopsForAdd(cmbShop)||!ComboLoadHelper.TryLoadProductsForAdd(cmbProduct)) Close();}
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var qty=InputHelper.ParseInt(txtQty.Text);
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbShop.SelectedValue,"Магазин"),ValidationHelper.Combo(cmbProduct.SelectedValue,"Товар"),ValidationHelper.NonNegative(qty,"Количество"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      ctx.Inventories.Add(new Inventory{IdShop=(int)cmbShop.SelectedValue!,IdProduct=(int)cmbProduct.SelectedValue!,Quantity=qty});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
