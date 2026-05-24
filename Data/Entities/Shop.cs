using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Shop
{
    public int IdShop { get; set; }

    public int IdBranch { get; set; }

    public string ShopName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? WorkingHours { get; set; }

    public virtual Branch IdBranchNavigation { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
