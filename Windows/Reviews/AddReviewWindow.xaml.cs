using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Reviews;

public partial class AddReviewWindow : Window
{
    public AddReviewWindow()
    {
        InitializeComponent();
        dpDate.SelectedDate = DateTime.Today;

        if (!ComboLoadHelper.TryLoadVisitorsForAdd(cmbVisitor))
        {
            Close();
            return;
        }

        try
        {
            using var context = new MuseumDbContext();
            cmbExhibition.ItemsSource = context.Exhibitions
                .Select(e => new { Id = (int?)e.IdExhibition, Name = e.ExhibitionName })
                .Prepend(new { Id = (int?)null, Name = "—" })
                .ToList();
            cmbExhibition.DisplayMemberPath = "Name";
            cmbExhibition.SelectedValuePath = "Id";
            cmbExhibition.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
            Close();
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

    private void btnAdd_Click(object sender, RoutedEventArgs e)
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
            context.Reviews.Add(new Review
            {
                IdVisitor = (int)cmbVisitor.SelectedValue!,
                IdExhibition = cmbExhibition.SelectedValue as int?,
                Rating = rating,
                Comment = TextHelper.TrimOrNull(txtComment.Text),
                ReviewDate = dpDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(dpDate.SelectedDate.Value)
                    : DateOnly.FromDateTime(DateTime.Today)
            });
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
