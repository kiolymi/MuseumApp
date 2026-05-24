using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class ExcursionTicket
{
    public int IdVisitor { get; set; }

    public int IdExcursion { get; set; }

    public DateOnly VisitDate { get; set; }

    public TimeOnly VisitTime { get; set; }

    public decimal? ActualCost { get; set; }

    public virtual Excursion IdExcursionNavigation { get; set; } = null!;

    public virtual Visitor IdVisitorNavigation { get; set; } = null!;
}
