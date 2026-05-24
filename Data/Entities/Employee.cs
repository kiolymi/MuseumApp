using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public int IdPosition { get; set; }

    public string? Phone { get; set; }

    public DateOnly BirthDate { get; set; }

    public string? EducationLevel { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Excursion> Excursions { get; set; } = new List<Excursion>();

    public virtual ICollection<ExhibitMovement> ExhibitMovements { get; set; } = new List<ExhibitMovement>();

    public virtual ICollection<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();

    public virtual Position IdPositionNavigation { get; set; } = null!;

    public virtual ICollection<Museum> Museums { get; set; } = new List<Museum>();

    public virtual ICollection<Restoration> Restorations { get; set; } = new List<Restoration>();
}
