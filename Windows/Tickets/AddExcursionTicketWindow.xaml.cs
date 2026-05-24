using System.Globalization;
using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Windows.Tickets;

public partial class AddExcursionTicketWindow : Window
{
    public AddExcursionTicketWindow()
    {
        InitializeComponent();
        dpVisitDate.SelectedDate = DateTime.Today;

        try
        {
            var context = new MuseumDbContext();
            cmbVisitor.ItemsSource = context.Visitors
                .Select(v => new { Id = v.IdVisitor, Name = $"{v.LastName} {v.FirstName} {v.MiddleName}" })
                .ToList();
            cmbExcursion.ItemsSource = context.Excursions
                .Select(e => new { Id = e.IdExcursion, Name = $"Экскурсия #{e.IdExcursion}" })
                .ToList();

            if (cmbVisitor.Items.Count > 0) cmbVisitor.SelectedIndex = 0;
            if (cmbExcursion.Items.Count > 0) cmbExcursion.SelectedIndex = 0;
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
            var visitTime = TimeOnly.Parse(txtVisitTime.Text.Trim(), CultureInfo.InvariantCulture);
            var context = new MuseumDbContext();
            var ticket = new ExcursionTicket
            {
                IdVisitor = (int)cmbVisitor.SelectedValue,
                IdExcursion = (int)cmbExcursion.SelectedValue,
                VisitDate = DateOnly.FromDateTime(dpVisitDate.SelectedDate ?? DateTime.Today),
                VisitTime = visitTime
            };
            context.ExcursionTickets.Add(ticket);
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
