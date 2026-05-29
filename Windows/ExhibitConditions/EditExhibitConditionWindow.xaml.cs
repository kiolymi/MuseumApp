using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.ExhibitConditions;

public partial class EditExhibitConditionWindow : Window
{
    private readonly int _id;

    public EditExhibitConditionWindow(ExhibitCondition condition)
    {
        InitializeComponent();
        _id = condition.IdCondition;
        txtName.Text = condition.ConditionName;
        txtDesc.Text = condition.Description;
    }

    private string? ValidateForm()
    {
        string? error;

        error = ValidationHelper.SafeText(txtName.Text, 255, "Название");
        if (error != null)
        {
            return error;
        }

        error = ValidationHelper.SafeText(txtDesc.Text, 255, "Описание");
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

            using var context = new MuseumDbContext();
            var item = context.ExhibitConditions.Find(_id);
            if (item == null)
            {
                return;
            }

            item.ConditionName = txtName.Text.Trim();
            item.Description = txtDesc.Text.Trim();

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
