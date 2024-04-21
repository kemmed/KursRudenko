using System;
using System.Collections.Generic;

namespace Kurs2.Models;

public partial class TypeProd
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<ProductAssortment> ProductAssortments { get; set; } = new List<ProductAssortment>();
}
