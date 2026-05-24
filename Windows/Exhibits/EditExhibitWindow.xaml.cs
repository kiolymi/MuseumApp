using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Exhibits;

public partial class EditExhibitWindow : Window
{
    private readonly int _id;

    public EditExhibitWindow(Exhibit selected)
    {
        InitializeComponent();
        _id = selected.IdExhibit;
        txtName.Text = selected.Name;
        if (selected.CreationDate.HasValue)
            dpCreationDate.SelectedDate = selected.CreationDate.Value.ToDateTime(TimeOnly.MinValue);

        var context = new MuseumDbContext();
        cmbCollection.ItemsSource = context.Collections
            .Select(c => new { Id = c.IdCollection, Name = c.CollectionName })
            .ToList();
        cmbAuthor.ItemsSource = context.Authors
            .Select(a => new { Id = (int?)a.IdAuthor, Name = $"{a.LastName} {a.FirstName}" })
            .ToList();
        cmbCondition.ItemsSource = context.ExhibitConditions
            .Select(c => new { Id = c.IdCondition, Name = c.ConditionName })
            .ToList();

        cmbCollection.SelectedValue = selected.IdCollection;
        cmbAuthor.SelectedValue = selected.IdAuthor;
        cmbCondition.SelectedValue = selected.IdCondition;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var item = context.Exhibits.Find(_id);
            if (item == null) return;

            item.Name = txtName.Text;
            item.IdCollection = (int)cmbCollection.SelectedValue;
            item.IdAuthor = cmbAuthor.SelectedValue as int?;
            item.IdCondition = (int)cmbCondition.SelectedValue;
            item.CreationDate = dpCreationDate.SelectedDate.HasValue
                ? DateOnly.FromDateTime(dpCreationDate.SelectedDate.Value)
                : null;

            context.SaveChanges();
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
