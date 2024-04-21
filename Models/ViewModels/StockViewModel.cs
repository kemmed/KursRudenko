using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs2.Models.ViewModels
{
    internal class StockViewModel
    {
        public ProductAttrib attrib { get; set; }
        public string name { get; set; } = "";

        public StockViewModel() { } 
        public StockViewModel(ProductAttrib attrib, string name)
        {
            this.attrib = attrib;
            this.name = name;
        }
    }
}
