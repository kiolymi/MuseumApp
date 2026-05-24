using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Reason
{
    public int IdReason { get; set; }

    public string ReasonDescription { get; set; } = null!;

    public virtual ICollection<ExhibitMovement> ExhibitMovements { get; set; } = new List<ExhibitMovement>();
}
