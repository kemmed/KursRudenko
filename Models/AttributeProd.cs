using System;
using System.Collections.Generic;

namespace Kurs2.Models;

public partial class AttributeProd
{
    public int AttributeId { get; set; }

    public string AttributeName { get; set; } = null!;

    public string? AttributeValueType { get; set; }

    public virtual ICollection<ProductAttrib> ProductAttribs { get; set; } = new List<ProductAttrib>();
}
