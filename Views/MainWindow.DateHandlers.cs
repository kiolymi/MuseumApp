using System.Windows;
using MuseumApp.Helpers;
using MuseumApp.Queries;

namespace MuseumApp.Views;

public partial class MainWindow
{
    private void btn_find_tickets_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (dpTicketFrom.SelectedDate == null || dpTicketTo.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату «от» и дату «до»");
                return;
            }

            var from = DateOnly.FromDateTime(dpTicketFrom.SelectedDate.Value);
            var to = DateOnly.FromDateTime(dpTicketTo.SelectedDate.Value);

            if (from > to)
            {
                MessageBox.Show("Дата «от» не может быть позже даты «до»");
                return;
            }

            var queries = new MuseumQueries();
            dg_exhibitionTickets.ItemsSource = queries.TicketsByPeriod(from, to);
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
        }
    }

    private void btn_show_all_tickets_Click(object sender, RoutedEventArgs e)
    {
        Load();
    }
}
