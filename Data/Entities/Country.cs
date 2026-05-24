using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Country
{
    public int IdCountry { get; set; }

    public string CountryName { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
