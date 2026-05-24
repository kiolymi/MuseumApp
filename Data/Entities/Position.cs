using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Position
{
    public int IdPosition { get; set; }

    public string PositionName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Salary { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
