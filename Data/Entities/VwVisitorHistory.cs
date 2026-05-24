using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class VwVisitorHistory
{
    public int? IdVisitor { get; set; }

    public string? VisitorName { get; set; }

    public string? ExhibitionName { get; set; }

    public DateOnly? VisitDate { get; set; }

    public decimal? TicketPrice { get; set; }

    public string? AppliedPrivilege { get; set; }
}
