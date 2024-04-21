using System;
using System.Collections.Generic;

namespace Kurs2.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public DateTime SaleDate { get; set; }

    public virtual ICollection<SaleProd> SaleProds { get; set; } = new List<SaleProd>();
}
