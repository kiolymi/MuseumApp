using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class VwActiveExhibition
{
    public int? IdExhibition { get; set; }

    public string? ExhibitionName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
