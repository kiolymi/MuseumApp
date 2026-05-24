using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Event
{
    public int IdEvent { get; set; }

    public string EventName { get; set; } = null!;

    public int IdBranch { get; set; }

    public int IdEmployee { get; set; }

    public DateOnly EventDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public int DurationMinutes { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<EventTicket> EventTickets { get; set; } = new List<EventTicket>();

    public virtual Branch IdBranchNavigation { get; set; } = null!;

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;
}
