using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class VwStorageOccupancy
{
    public int? IdStorage { get; set; }

    public string? StorageName { get; set; }

    public string? BranchName { get; set; }

    public long? ExhibitCount { get; set; }

    public string? CollectionsHeld { get; set; }

    public decimal? AvgTemperature { get; set; }

    public decimal? AvgHumidity { get; set; }
}
