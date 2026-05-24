using System.Globalization;
using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Tickets;

public partial class EditExhibitionTicketWindow : Window
{
    private readonly int _idVisitor;
    private readonly int _idExhibition;

    public EditExhibitionTicketWindow(ExhibitionTicket selected)
    {
        InitializeComponent();
        _idVisitor = selected.IdVisitor;
        _idExhibition = selected.IdExhibition;
        lblVisitor.Text = selected.IdVisitor.ToString();
        lblExhibition.Text = selected.IdExhibition.ToString();
        if (selected.VisitDate.HasValue)
            dpVisitDate.SelectedDate = selected.VisitDate.Value.ToDateTime(TimeOnly.MinValue);
        txtVisitTime.Text = selected.VisitTime?.ToString("HH:mm", CultureInfo.InvariantCulture) ?? "10:00";
        txtActualCost.Text = selected.ActualCost.ToString();
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var visitTime = TimeOnly.Parse(txtVisitTime.Text.Trim(), CultureInfo.InvariantCulture);
            var context = new MuseumDbContext();
            var item = context.ExhibitionTickets.Find(_idExhibition, _idVisitor);
            if (item == null) return;

            item.VisitDate = DateOnly.FromDateTime(dpVisitDate.SelectedDate ?? DateTime.Today);
            item.VisitTime = visitTime;
            item.ActualCost = InputHelper.ParseDecimal(txtActualCost.Text);

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
