using System.Globalization;
using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Tickets;

public partial class EditExcursionTicketWindow : Window
{
    private readonly int _idVisitor;
    private readonly int _idExcursion;

    public EditExcursionTicketWindow(ExcursionTicket selected)
    {
        InitializeComponent();
        _idVisitor = selected.IdVisitor;
        _idExcursion = selected.IdExcursion;
        lblVisitor.Text = selected.IdVisitor.ToString();
        lblExcursion.Text = selected.IdExcursion.ToString();
        dpVisitDate.SelectedDate = selected.VisitDate.ToDateTime(TimeOnly.MinValue);
        txtVisitTime.Text = selected.VisitTime.ToString("HH:mm", CultureInfo.InvariantCulture);
        txtActualCost.Text = selected.ActualCost?.ToString() ?? "0";
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var visitTime = TimeOnly.Parse(txtVisitTime.Text.Trim(), CultureInfo.InvariantCulture);
            var context = new MuseumDbContext();
            var item = context.ExcursionTickets.Find(_idVisitor, _idExcursion);
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
