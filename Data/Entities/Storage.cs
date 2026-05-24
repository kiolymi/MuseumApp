using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Storage
{
    public int IdStorage { get; set; }

    public string StorageName { get; set; } = null!;

    public int IdBranch { get; set; }

    public decimal? Temperature { get; set; }

    public decimal? Humidity { get; set; }

    public virtual ICollection<ExhibitMovement> ExhibitMovementFromStorages { get; set; } = new List<ExhibitMovement>();

    public virtual ICollection<ExhibitMovement> ExhibitMovementToStorages { get; set; } = new List<ExhibitMovement>();

    public virtual Branch IdBranchNavigation { get; set; } = null!;
}
