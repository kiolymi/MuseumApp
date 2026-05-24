using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Restoration
{
    public int IdRestoration { get; set; }

    public int IdExhibit { get; set; }

    public int IdRestorer { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? Cost { get; set; }

    public string? WorkDescription { get; set; }

    public virtual Exhibit IdExhibitNavigation { get; set; } = null!;

    public virtual Employee IdRestorerNavigation { get; set; } = null!;
}
