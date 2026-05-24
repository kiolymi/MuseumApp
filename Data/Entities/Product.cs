using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Product
{
    public int IdProduct { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int IdCompanySupplier { get; set; }

    public virtual Company IdCompanySupplierNavigation { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
