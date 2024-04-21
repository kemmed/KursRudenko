using System;
using System.Collections.Generic;

namespace Kurs2.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public string Place { get; set; } = null!;

    public int Count { get; set; }

    public virtual ICollection<ProductAttrib> ProductAttribs { get; set; } = new List<ProductAttrib>();
}
