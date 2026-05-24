using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Branch
{
    public int IdBranch { get; set; }

    public int IdMuseum { get; set; }

    public string BranchName { get; set; } = null!;

    public int IdAddress { get; set; }

    public string? Phone { get; set; }

    public int IdResponsible { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Hall> Halls { get; set; } = new List<Hall>();

    public virtual Adress IdAddressNavigation { get; set; } = null!;

    public virtual Museum IdMuseumNavigation { get; set; } = null!;

    public virtual Employee IdResponsibleNavigation { get; set; } = null!;

    public virtual Shop? Shop { get; set; }

    public virtual ICollection<Storage> Storages { get; set; } = new List<Storage>();
}
