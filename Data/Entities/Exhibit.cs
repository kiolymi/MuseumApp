using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Exhibit
{
    public int IdExhibit { get; set; }

    public string Name { get; set; } = null!;

    public int IdCollection { get; set; }

    public int? IdAuthor { get; set; }

    public int IdCondition { get; set; }

    public DateOnly? CreationDate { get; set; }

    public virtual ICollection<ExhibitMovement> ExhibitMovements { get; set; } = new List<ExhibitMovement>();

    public virtual Collection IdCollectionNavigation { get; set; } = null!;

    public virtual ExhibitCondition IdConditionNavigation { get; set; } = null!;

    public virtual ICollection<Restoration> Restorations { get; set; } = new List<Restoration>();

    public virtual ICollection<Author> IdAuthors { get; set; } = new List<Author>();

    public virtual ICollection<Exhibition> IdExhibitions { get; set; } = new List<Exhibition>();

    public virtual ICollection<Material> IdMaterials { get; set; } = new List<Material>();
}
