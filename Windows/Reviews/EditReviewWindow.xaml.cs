using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Reviews;

public partial class EditReviewWindow : Window
{
    private readonly int _id;

    public EditReviewWindow(Review review)
    {
        InitializeComponent();
        _id = review.IdReview;
        txtRating.Text = review.Rating.ToString();
        txtComment.Text = review.Comment ?? "";
        dpDate.SelectedDate = review.ReviewDate.ToDateTime(TimeOnly.MinValue);

        ComboLoadHelper.LoadVisitors(cmbVisitor, review.IdVisitor);

        try
        {
            using var context = new MuseumDbContext();
            cmbExhibition.ItemsSource = context.Exhibitions
                .Select(e => new { Id = (int?)e.IdExhibition, Name = e.ExhibitionName })
                .Prepend(new { Id = (int?)null, Name = "—" })
                .ToList();
            cmbExhibition.SelectedValue = review.IdExhibition;
        }
        catch
        {
            // ignore combo load errors on edit
        }
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.Combo(cmbVisitor.SelectedValue, "Посетитель");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.OptionalSafeText(txtComment.Text, 1000, "Комментарий");
        if (error != null)
        {
            return error;
        }

        return null;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var error = ValidateForm();
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var rating = InputHelper.ParseInt(txtRating.Text);
            var ratingError = ValidationHelper.Positive(rating, "Оценка");
            if (ratingError != null)
            {
                MessageBox.Show(ratingError);
                return;
            }

            using var context = new MuseumDbContext();
            var item = context.Reviews.Find(_id);
            if (item == null)
            {
                return;
            }

            item.IdVisitor = (int)cmbVisitor.SelectedValue!;
            item.IdExhibition = cmbExhibition.SelectedValue as int?;
            item.Rating = rating;
            item.Comment = TextHelper.TrimOrNull(txtComment.Text);
            if (dpDate.SelectedDate.HasValue)
            {
                item.ReviewDate = DateOnly.FromDateTime(dpDate.SelectedDate.Value);
            }

            context.SaveChanges();
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
        }
    }
}
