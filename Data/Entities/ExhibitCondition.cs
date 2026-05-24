using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class ExhibitCondition
{
    public int IdCondition { get; set; }

    public string ConditionName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Exhibit> Exhibits { get; set; } = new List<Exhibit>();
}
