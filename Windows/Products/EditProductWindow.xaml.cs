using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Products;
public partial class EditProductWindow : Window {
  private readonly int _id;
  public EditProductWindow(Product p){InitializeComponent();_id=p.IdProduct;txtName.Text=p.ProductName;txtDesc.Text=p.Description??"";txtPrice.Text=p.Price.ToString();
    ComboLoadHelper.LoadCompanies(cmbCompany,p.IdCompanySupplier);}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var price=InputHelper.ParseDecimal(txtPrice.Text);
      var err=ValidationHelper.First(ValidationHelper.NotEmpty(txtName.Text,"Название"),ValidationHelper.NonNegative(price,"Цена"),ValidationHelper.Combo(cmbCompany.SelectedValue,"Поставщик"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext(); var item=ctx.Products.Find(_id); if(item==null)return;
      item.ProductName=txtName.Text.Trim(); item.Description=string.IsNullOrWhiteSpace(txtDesc.Text)?null:txtDesc.Text.Trim(); item.Price=price; item.IdCompanySupplier=(int)cmbCompany.SelectedValue!;
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
