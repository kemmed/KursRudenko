using System;
using System.Collections.Generic;

namespace Kurs2.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual ICollection<OrderProd> OrderProds { get; set; } = new List<OrderProd>();
}
