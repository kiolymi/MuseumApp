using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Visitor
{
    public int IdVisitor { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? IdPrivilege { get; set; }

    public virtual ICollection<EventTicket> EventTickets { get; set; } = new List<EventTicket>();

    public virtual ICollection<ExcursionTicket> ExcursionTickets { get; set; } = new List<ExcursionTicket>();

    public virtual ICollection<Excursion> Excursions { get; set; } = new List<Excursion>();

    public virtual ICollection<ExhibitionTicket> ExhibitionTickets { get; set; } = new List<ExhibitionTicket>();

    public virtual Privilege? IdPrivilegeNavigation { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
