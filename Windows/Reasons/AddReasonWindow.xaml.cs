using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Reasons;

public partial class AddReasonWindow : Window
{
    public AddReasonWindow()
    {
        InitializeComponent();
    }

    private string? ValidateForm()
    {
        return ValidationHelper.SafeText(txtValue.Text, 255, "Описание");
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

            using var context = new MuseumDbContext();
            context.Reasons.Add(new Reason { ReasonDescription = txtValue.Text.Trim() });
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
