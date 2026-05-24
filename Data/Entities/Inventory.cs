using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class Inventory
{
    public int IdShop { get; set; }

    public int IdProduct { get; set; }

    public int Quantity { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual Shop IdShopNavigation { get; set; } = null!;
}
