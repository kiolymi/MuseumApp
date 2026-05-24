using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Exhibits;

public partial class AddExhibitWindow : Window
{
    public AddExhibitWindow()
    {
        InitializeComponent();

        try
        {
            var context = new MuseumDbContext();
            cmbCollection.ItemsSource = context.Collections
                .Select(c => new { Id = c.IdCollection, Name = c.CollectionName })
                .ToList();
            var authors = context.Authors
                .Select(a => new { Id = (int?)a.IdAuthor, Name = $"{a.LastName} {a.FirstName}".Trim() })
                .ToList();
            authors.Insert(0, new { Id = (int?)null, Name = "—" });
            cmbAuthor.ItemsSource = authors;
            cmbCondition.ItemsSource = context.ExhibitConditions
                .Select(c => new { Id = c.IdCondition, Name = c.ConditionName })
                .ToList();
            if (cmbCollection.Items.Count > 0) cmbCollection.SelectedIndex = 0;
            if (cmbAuthor.Items.Count > 0) cmbAuthor.SelectedIndex = 0;
            if (cmbCondition.Items.Count > 0) cmbCondition.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка загрузки: " + ex.Message);
            Close();
        }
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = new MuseumDbContext();
            var exhibit = new Exhibit
            {
                IdExhibit = (context.Exhibits.Max(x => (int?)x.IdExhibit) ?? 0) + 1,
                Name = txtName.Text,
                IdCollection = (int)cmbCollection.SelectedValue,
                IdAuthor = cmbAuthor.SelectedValue as int?,
                IdCondition = (int)cmbCondition.SelectedValue,
                CreationDate = dpCreationDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(dpCreationDate.SelectedDate.Value)
                    : null
            };
            context.Exhibits.Add(exhibit);
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
