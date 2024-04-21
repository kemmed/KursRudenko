using Kurs2.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs2.Models.ViewModels
{
    internal class ProductsInOrderViewModels
    {
        private readonly Kurs2RudenkoContext _context;
        public int Id { get; set; }
        public string name { get; set; } = "";
        public int count { get; set; }
        public ProductsInOrderViewModels(int Id, Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            this.Id = Id;
            this.count = _context.OrderProds.FirstOrDefault(x=>x.ProductId==Id).Count;

            var attribs = _context.ProductAttribs
                .Include(x => x.ProdAssort)
                .Include(x => x.Attribute)
                .ToList();

            var groupedAttribs = attribs
                .Where(x => x.ProdAssortId == Id)
                .ToList();

            name += _context.ProductAssortments.FirstOrDefault(x => x.ProdAssortId == Id).ProdAssortName + 
                " " + 
                string.Join(" ", groupedAttribs.Select(attr => 
                                                        $"{attr.AttributeValue.ToLower()} {attr.Attribute.AttributeValueType}"));

        }
    }
}
