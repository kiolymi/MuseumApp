using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Company
{
    public int IdCompany { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Inn { get; set; }

    public string? LegalAddress { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
