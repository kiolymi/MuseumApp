using MuseumApp.Data;
using MuseumApp.Data.Entities;

namespace MuseumApp.Queries;

public class MuseumQueries
{
    public List<ExhibitionTicket> TicketsByPeriod(DateOnly from, DateOnly to)
    {
        var context = new MuseumDbContext();
        return context.ExhibitionTickets
            .Where(t => t.VisitDate >= from && t.VisitDate <= to)
            .OrderBy(t => t.VisitDate)
            .ToList();
    }
}
