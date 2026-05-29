using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.EventTickets;

public partial class AddEventTicketWindow : Window
{
    public AddEventTicketWindow()
    {
        InitializeComponent();
        dpPurchase.SelectedDate = DateTime.Today;

        if (!ComboLoadHelper.TryLoadVisitorsForAdd(cmbVisitor)
            || !ComboLoadHelper.TryLoadEventsForAdd(cmbEvent))
        {
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

        error = ValidationHelper.Combo(cmbEvent.SelectedValue, "Мероприятие");
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

            var price = InputHelper.ParseDecimal(txtPrice.Text);
            var priceError = ValidationHelper.NonNegative(price, "Цена");
            if (priceError != null)
            {
                MessageBox.Show(priceError);
                return;
            }

            using var context = new MuseumDbContext();
            context.EventTickets.Add(new EventTicket
            {
                IdVisitor = (int)cmbVisitor.SelectedValue!,
                IdEvent = (int)cmbEvent.SelectedValue!,
                PurchaseDate = DateOnly.FromDateTime(dpPurchase.SelectedDate ?? DateTime.Today),
                ActualPrice = price
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
