using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class VwExhibitFullInfo
{
    public int? IdExhibit { get; set; }

    public string? ExhibitName { get; set; }

    public string? Author { get; set; }

    public string? CollectionName { get; set; }

    public string? ConditionName { get; set; }

    public string? Materials { get; set; }

    public string? CurrentStorage { get; set; }

    public DateOnly? CreationDate { get; set; }
}
