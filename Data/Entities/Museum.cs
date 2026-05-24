using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Museum
{
    public int IdMuseum { get; set; }

    public string Name { get; set; } = null!;

    public int IdDirector { get; set; }

    public int IdAddress { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual Adress IdAddressNavigation { get; set; } = null!;

    public virtual Employee IdDirectorNavigation { get; set; } = null!;
}
