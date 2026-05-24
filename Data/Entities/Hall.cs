using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Hall
{
    public int IdHall { get; set; }

    public string HallName { get; set; } = null!;

    public int IdBranch { get; set; }

    public double? Area { get; set; }

    public int? Capacity { get; set; }

    public virtual Branch IdBranchNavigation { get; set; } = null!;

    public virtual ICollection<Exhibition> IdExhibitions { get; set; } = new List<Exhibition>();
}
