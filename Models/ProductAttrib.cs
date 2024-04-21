using System;
using System.Collections.Generic;

namespace Kurs2.Models;

public partial class ProductAttrib
{
    public int ProdAttribId { get; set; }

    public int ProdAssortId { get; set; }

    public int AttributeId { get; set; }

    public string AttributeValue { get; set; } = null!;

    public int? StockId { get; set; }

    public double PriceCoeff { get; set; }

    public virtual AttributeProd Attribute { get; set; } = null!;

    public virtual ProductAssortment ProdAssort { get; set; } = null!;

    public virtual Stock? Stock { get; set; }
}
