using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Excursion
{
    public int IdExcursion { get; set; }

    public int IdExhibition { get; set; }

    public int IdGuide { get; set; }

    public int Duration { get; set; }

    public int IdCustomer { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<ExcursionTicket> ExcursionTickets { get; set; } = new List<ExcursionTicket>();

    public virtual Visitor IdCustomerNavigation { get; set; } = null!;

    public virtual Exhibition IdExhibitionNavigation { get; set; } = null!;

    public virtual Employee IdGuideNavigation { get; set; } = null!;
}
