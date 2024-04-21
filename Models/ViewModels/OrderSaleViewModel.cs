using Kurs2.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs2.Models.ViewModels
{

    internal class OrderSaleViewModel
    {
        private readonly Kurs2RudenkoContext _context;
        public int Id { get; set; }
        public string name { get; set; } = "";
        public decimal price { get; set; }
        public int count { get; set; }
        public OrderSaleViewModel(int Id, Kurs2RudenkoContext kurs2RudenkoContext, int count)
        {
            _context = kurs2RudenkoContext;
            this.Id = Id;
            this.count = count;

            var attribs = _context.ProductAttribs
                .Include(x => x.ProdAssort)
                .Include(x => x.Attribute)
                .ToList();
            var groupedAttribs = attribs
                .Where(x=> x.ProdAssortId == Id)
                .ToList();

            name += _context.ProductAssortments.FirstOrDefault(x=>x.ProdAssortId== Id).ProdAssortName + 
                " " + 
                string.Join(" ", groupedAttribs.Select(attr => 
                                                        $"{attr.AttributeValue.ToLower()} {attr.Attribute.AttributeValueType}"));

            this.price = _context.ProductAssortments.FirstOrDefault(x => x.ProdAssortId == Id).ProdBasePrice * 
                (decimal)groupedAttribs.Aggregate(1.0, (acc, x) => acc * x.PriceCoeff);

        }
    }
}
