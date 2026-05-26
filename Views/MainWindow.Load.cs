using System.Windows;
using MuseumApp.Data;
using MuseumApp.Helpers;

namespace MuseumApp.Views;

public partial class MainWindow
{
    public void Load()
    {
        try
        {
            var context = new MuseumDbContext();

            if (SessionUser.Role == "admin_museum")
            {
                dg_exhibitions.ItemsSource = context.Exhibitions.ToList();
                dg_exhibits.ItemsSource = context.Exhibits.ToList();
                dg_collections.ItemsSource = context.Collections.ToList();
                dg_halls.ItemsSource = context.Halls.ToList();
                dg_authors.ItemsSource = context.Authors.ToList();
                dg_visitors.ItemsSource = context.Visitors.ToList();
                dg_exhibitionTickets.ItemsSource = context.ExhibitionTickets.ToList();
                dg_excursionTickets.ItemsSource = context.ExcursionTickets.ToList();
                dg_excursions.ItemsSource = context.Excursions.ToList();
                dg_privileges.ItemsSource = context.Privileges.ToList();
                dg_employees.ItemsSource = context.Employees.ToList();
                dg_events.ItemsSource = context.Events.ToList();
            }
            else if (SessionUser.Role == "curator_museum")
            {
                dg_exhibitions.ItemsSource = context.Exhibitions.ToList();
                dg_exhibits.ItemsSource = context.Exhibits.ToList();
                dg_collections.ItemsSource = context.Collections.ToList();
                dg_halls.ItemsSource = context.Halls.ToList();
                dg_authors.ItemsSource = context.Authors.ToList();
            }
            else if (SessionUser.Role == "cashier_museum")
            {
                dg_exhibitions.ItemsSource = context.Exhibitions.ToList();
                dg_excursions.ItemsSource = context.Excursions.ToList();
                dg_visitors.ItemsSource = context.Visitors.ToList();
                dg_exhibitionTickets.ItemsSource = context.ExhibitionTickets.ToList();
                dg_excursionTickets.ItemsSource = context.ExcursionTickets.ToList();
                dg_privileges.ItemsSource = context.Privileges.ToList();
            }
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
        }
    }
}
