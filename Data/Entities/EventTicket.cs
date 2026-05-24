using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class EventTicket
{
    public int IdVisitor { get; set; }

    public int IdEvent { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public decimal ActualPrice { get; set; }

    public virtual Event IdEventNavigation { get; set; } = null!;

    public virtual Visitor IdVisitorNavigation { get; set; } = null!;
}
