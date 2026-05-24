using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class ExhibitMovement
{
    public int IdMovement { get; set; }

    public int IdExhibit { get; set; }

    public int IdResponsible { get; set; }

    public int FromStorageId { get; set; }

    public int ToStorageId { get; set; }

    public DateOnly? MovementDate { get; set; }

    public int IdReason { get; set; }

    public virtual Storage FromStorage { get; set; } = null!;

    public virtual Exhibit IdExhibitNavigation { get; set; } = null!;

    public virtual Reason IdReasonNavigation { get; set; } = null!;

    public virtual Employee IdResponsibleNavigation { get; set; } = null!;

    public virtual Storage ToStorage { get; set; } = null!;
}
