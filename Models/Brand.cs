using System;
using System.Collections.Generic;

namespace Kurs2.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public virtual ICollection<ProductAssortment> ProductAssortments { get; set; } = new List<ProductAssortment>();
}
