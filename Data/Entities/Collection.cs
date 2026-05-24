using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Collection
{
    public int IdCollection { get; set; }

    public string CollectionName { get; set; } = null!;

    public int IdKeeper { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();

    public virtual Employee IdKeeperNavigation { get; set; } = null!;
}
