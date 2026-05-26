using System.Globalization;
using System.Windows;
using MuseumApp.Data;
using MuseumApp.Data.Entities;
using MuseumApp.Helpers;

namespace MuseumApp.Windows.Tickets;

public partial class AddExhibitionTicketWindow : Window
{
    public AddExhibitionTicketWindow()
    {
        InitializeComponent();
        dpVisitDate.SelectedDate = DateTime.Today;

        try
        {
            var context = new MuseumDbContext();
            cmbVisitor.ItemsSource = context.Visitors
                .Select(v => new { Id = v.IdVisitor, Name = $"{v.LastName} {v.FirstName} {v.MiddleName}" })
                .ToList();
            cmbExhibition.ItemsSource = context.Exhibitions
                .Select(e => new { Id = e.IdExhibition, Name = e.ExhibitionName })
                .ToList();

            if (cmbVisitor.Items.Count > 0) cmbVisitor.SelectedIndex = 0;
            if (cmbExhibition.Items.Count > 0) cmbExhibition.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            DbErrorHelper.Show(ex);
            Close();
        }
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var error = ValidationHelper.First(
                ValidationHelper.Combo(cmbVisitor.SelectedValue, "Посетитель"),
                ValidationHelper.Combo(cmbExhibition.SelectedValue, "Выставка"),
                ValidationHelper.VisitDate(dpVisitDate.SelectedDate));
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            var visitTime = TimeOnly.Parse(txtVisitTime.Text.Trim(), CultureInfo.InvariantCulture);
            var context = new MuseumDbContext();
            var exhibition = context.Exhibitions.Find((int)cmbExhibition.SelectedValue!);
            var ticket = new ExhibitionTicket
            {
                IdVisitor = (int)cmbVisitor.SelectedValue!,
                IdExhibition = (int)cmbExhibition.SelectedValue!,
                VisitDate = DateOnly.FromDateTime(dpVisitDate.SelectedDate ?? DateTime.Today),
                VisitTime = visitTime,
                ActualCost = exhibition?.Price ?? 0
            };
            context.ExhibitionTickets.Add(ticket);
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
