using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Products;
public partial class AddProductWindow : Window {
  public AddProductWindow(){InitializeComponent(); if(!ComboLoadHelper.TryLoadCompaniesForAdd(cmbCompany)) Close();}
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var price=InputHelper.ParseDecimal(txtPrice.Text);
      var err=ValidationHelper.First(ValidationHelper.NotEmpty(txtName.Text,"Название"),ValidationHelper.NonNegative(price,"Цена"),ValidationHelper.Combo(cmbCompany.SelectedValue,"Поставщик"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      ctx.Products.Add(new Product{ProductName=txtName.Text.Trim(),Description=string.IsNullOrWhiteSpace(txtDesc.Text)?null:txtDesc.Text.Trim(),Price=price,IdCompanySupplier=(int)cmbCompany.SelectedValue!});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
