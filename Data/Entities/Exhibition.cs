using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Exhibition
{
    public int IdExhibition { get; set; }

    public string ExhibitionName { get; set; } = null!;

    public int IdCurator { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Theme { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Excursion> Excursions { get; set; } = new List<Excursion>();

    public virtual ICollection<ExhibitionTicket> ExhibitionTickets { get; set; } = new List<ExhibitionTicket>();

    public virtual Employee IdCuratorNavigation { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Exhibit> IdExhibits { get; set; } = new List<Exhibit>();

    public virtual ICollection<Hall> IdHalls { get; set; } = new List<Hall>();
}
