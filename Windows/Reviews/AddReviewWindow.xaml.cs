using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Reviews;
public partial class AddReviewWindow : Window {
  public AddReviewWindow(){InitializeComponent(); dpDate.SelectedDate=DateTime.Today;
    if(!ComboLoadHelper.TryLoadVisitorsForAdd(cmbVisitor)) Close();
    try{ using var ctx=new MuseumDbContext();
      cmbExhibition.ItemsSource=ctx.Exhibitions.Select(e=>new{Id=(int?)e.IdExhibition,Name=e.ExhibitionName}).Prepend(new{Id=(int?)null,Name="—"}).ToList();
      cmbExhibition.DisplayMemberPath="Name"; cmbExhibition.SelectedValuePath="Id"; cmbExhibition.SelectedIndex=0;
    }catch(Exception ex){DbErrorHelper.Show(ex); Close();}}
  private void btnAdd_Click(object sender,RoutedEventArgs e){
    try{
      var rating=InputHelper.ParseInt(txtRating.Text);
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbVisitor.SelectedValue,"Посетитель"),ValidationHelper.Positive(rating,"Оценка"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext();
      ctx.Reviews.Add(new Review{IdVisitor=(int)cmbVisitor.SelectedValue!,IdExhibition=cmbExhibition.SelectedValue as int?,Rating=rating,
        Comment=string.IsNullOrWhiteSpace(txtComment.Text)?null:txtComment.Text.Trim(),
        ReviewDate=dpDate.SelectedDate.HasValue?DateOnly.FromDateTime(dpDate.SelectedDate.Value):DateOnly.FromDateTime(DateTime.Today)});
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
