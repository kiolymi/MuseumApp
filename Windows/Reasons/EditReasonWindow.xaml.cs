using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Reasons;

public partial class EditReasonWindow : Window
{
    private readonly int _id;

    public EditReasonWindow(Reason selected)
    {
        InitializeComponent();
        _id = selected.IdReason;
        txtValue.Text = selected.ReasonDescription;
    }

    private string? ValidateForm()
    {
        return ValidationHelper.SafeText(txtValue.Text, 255, "Описание");
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

            using var context = new MuseumDbContext();
            var item = context.Reasons.Find(_id);
            if (item == null)
            {
                return;
            }

            item.ReasonDescription = txtValue.Text.Trim();
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
