using System;
using System.Collections.Generic;

namespace Kurs2.Models;

public partial class SaleProd
{
    public int SaleProdId { get; set; }

    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }

    public virtual ProductAssortment Product { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
