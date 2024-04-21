using System;
using System.Collections.Generic;

namespace Kurs2.Models;

public partial class OrderProd
{
    public int OrderProdId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ProductAssortment Product { get; set; } = null!;
}
