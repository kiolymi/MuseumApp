using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class VwEmployeeDuty
{
    public int? IdEmployee { get; set; }

    public string? FullName { get; set; }

    public string? PositionName { get; set; }

    public string? Phone { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? EducationLevel { get; set; }

    public long? ExcursionsConducted { get; set; }

    public long? ExhibitionsCurated { get; set; }

    public long? CollectionsKept { get; set; }
}
