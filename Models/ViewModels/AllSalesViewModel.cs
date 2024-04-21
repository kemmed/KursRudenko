using Kurs2.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kurs2.Models.ViewModels
{
    internal class AllSalesViewModel
    {
        private readonly Kurs2RudenkoContext _context;
        public int Id { get; set; }
        public decimal summ { get; set; }
        public DateTime date { get; set; }

        public string formattedDate {
            get => date.ToString("d.MM.yyyy h:m");
        }

        public AllSalesViewModel(int Id, Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            this.Id = Id;
            summ = 0;
            date = _context.Sales.FirstOrDefault(x => x.SaleId == Id).SaleDate;

            var saleProducts = _context.SaleProds
                .Include(op => op.Product)
                .ThenInclude(p => p.ProductAttribs)
                .Where(op => op.SaleId == Id)
                .ToList();

            foreach (var saleProduct in saleProducts)
            {
                decimal productPrice = saleProduct.Product.ProdBasePrice;

                foreach (var productAttrib in saleProduct.Product.ProductAttribs)
                {
                    productPrice = ((decimal)productAttrib.PriceCoeff * productPrice);
                }
                summ += productPrice * saleProduct.Count;
            }
        }

    }
}
