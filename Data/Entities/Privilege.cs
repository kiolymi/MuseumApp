using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Privilege
{
    public int IdPrivilege { get; set; }

    public string PrivilegeName { get; set; } = null!;

    public double? DiscountRate { get; set; }

    public virtual ICollection<Visitor> Visitors { get; set; } = new List<Visitor>();
}
