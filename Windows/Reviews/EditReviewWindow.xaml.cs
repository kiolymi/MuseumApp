using System.Windows;
using MuseumApp.Data; using MuseumApp.Data.Entities; using MuseumApp.Helpers;
namespace MuseumApp.Windows.Reviews;
public partial class EditReviewWindow : Window {
  private readonly int _id;
  public EditReviewWindow(Review r){InitializeComponent();_id=r.IdReview;txtRating.Text=r.Rating.ToString();txtComment.Text=r.Comment??"";
    dpDate.SelectedDate=r.ReviewDate.ToDateTime(TimeOnly.MinValue); ComboLoadHelper.LoadVisitors(cmbVisitor,r.IdVisitor);
    try{ using var ctx=new MuseumDbContext();
      cmbExhibition.ItemsSource=ctx.Exhibitions.Select(e=>new{Id=(int?)e.IdExhibition,Name=e.ExhibitionName}).Prepend(new{Id=(int?)null,Name="—"}).ToList();
      cmbExhibition.SelectedValue=r.IdExhibition;}catch{}}
  private void btnSave_Click(object sender,RoutedEventArgs e){
    try{
      var rating=InputHelper.ParseInt(txtRating.Text);
      var err=ValidationHelper.First(ValidationHelper.Combo(cmbVisitor.SelectedValue,"Посетитель"),ValidationHelper.Positive(rating,"Оценка"));
      if(err!=null){MessageBox.Show(err);return;}
      using var ctx=new MuseumDbContext(); var item=ctx.Reviews.Find(_id); if(item==null)return;
      item.IdVisitor=(int)cmbVisitor.SelectedValue!; item.IdExhibition=cmbExhibition.SelectedValue as int?; item.Rating=rating;
      item.Comment=string.IsNullOrWhiteSpace(txtComment.Text)?null:txtComment.Text.Trim();
      item.ReviewDate=dpDate.SelectedDate.HasValue?DateOnly.FromDateTime(dpDate.SelectedDate.Value):item.ReviewDate;
      ctx.SaveChanges(); DialogResult=true; Close();
    }catch(Exception ex){DbErrorHelper.Show(ex);}
  }
}
