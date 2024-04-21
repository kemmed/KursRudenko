using System;
using System.Collections.Generic;

namespace Kurs2.Models;

public partial class ProductAssortment
{
    public int ProdAssortId { get; set; }

    public string ProdAssortName { get; set; } = null!;

    public string? ProdAssortDescription { get; set; }

    public int ВrandId { get; set; }

    public int TypeId { get; set; }

    public decimal ProdBasePrice { get; set; }

    public virtual ICollection<OrderProd> OrderProds { get; set; } = new List<OrderProd>();

    public virtual ICollection<ProductAttrib> ProductAttribs { get; set; } = new List<ProductAttrib>();

    public virtual ICollection<SaleProd> SaleProds { get; set; } = new List<SaleProd>();

    public virtual TypeProd Type { get; set; } = null!;

    public virtual Brand Вrand { get; set; } = null!;
}
