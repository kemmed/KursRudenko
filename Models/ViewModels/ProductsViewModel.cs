using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs2.Models.ViewModels
{
    class ProductsViewModel
    {
        public ProductAssortment Product {  get; set; }
        public int? Count { get; set; }
        public decimal Price { get; set; }

        public ProductsViewModel() { }

        public ProductsViewModel(ProductAssortment product, int? count)
        {
            Product = product;
            Count = count;
            Price = Product.ProdBasePrice;

            foreach (var productAttrib in Product.ProductAttribs)
            {
                Price = ((decimal)productAttrib.PriceCoeff * Price);
            }

        }
    }
}
