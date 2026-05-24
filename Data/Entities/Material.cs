using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Material
{
    public int IdMaterial { get; set; }

    public string MaterialName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Exhibit> IdExhibits { get; set; } = new List<Exhibit>();
}
