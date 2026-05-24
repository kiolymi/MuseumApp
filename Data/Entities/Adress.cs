using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Adress
{
    public int IdAddress { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string House { get; set; } = null!;

    public string? PostalCode { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Museum> Museums { get; set; } = new List<Museum>();
}
