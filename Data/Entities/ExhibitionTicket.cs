using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class ExhibitionTicket
{
    public int IdVisitor { get; set; }

    public int IdExhibition { get; set; }

    public decimal ActualCost { get; set; }

    public DateOnly? VisitDate { get; set; }

    public TimeOnly? VisitTime { get; set; }

    public virtual Exhibition IdExhibitionNavigation { get; set; } = null!;

    public virtual Visitor IdVisitorNavigation { get; set; } = null!;
}
