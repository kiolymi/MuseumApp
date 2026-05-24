using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Author
{
    public int IdAuthor { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public int IdCountry { get; set; }

    public virtual Country IdCountryNavigation { get; set; } = null!;

    public virtual ICollection<Exhibit> IdExhs { get; set; } = new List<Exhibit>();
}
