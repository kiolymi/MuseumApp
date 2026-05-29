using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.EventTickets;

public partial class EditEventTicketWindow : Window
{
    private readonly int _visitor;
    private readonly int _event;

    public EditEventTicketWindow(EventTicket ticket)
    {
        InitializeComponent();
        _visitor = ticket.IdVisitor;
        _event = ticket.IdEvent;
        txtPrice.Text = ticket.ActualPrice.ToString();
        dpPurchase.SelectedDate = ticket.PurchaseDate.ToDateTime(TimeOnly.MinValue);

        ComboLoadHelper.LoadVisitors(cmbVisitor, ticket.IdVisitor);
        ComboLoadHelper.LoadEvents(cmbEvent, ticket.IdEvent);
    }

    private string? ValidateForm()
    {
        var price = InputHelper.ParseDecimal(txtPrice.Text);
        return ValidationHelper.NonNegative(price, "Цена");
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

            var price = InputHelper.ParseDecimal(txtPrice.Text);

            using var context = new MuseumDbContext();
            var item = context.EventTickets.Find(_visitor, _event);
            if (item == null)
            {
                return;
            }

            item.PurchaseDate = DateOnly.FromDateTime(dpPurchase.SelectedDate ?? DateTime.Today);
            item.ActualPrice = price;

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
