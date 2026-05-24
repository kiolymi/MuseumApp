using System;
using System.Collections.Generic;

namespace MuseumApp.Data.Entities;

public partial class VwProductStock
{
    public int? IdShop { get; set; }

    public string? ShopName { get; set; }

    public int? IdProduct { get; set; }

    public string? ProductName { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? TotalValue { get; set; }
}
