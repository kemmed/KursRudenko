using Kurs2.Database;
using Kurs2.Models.ViewModels;
using Kurs2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kurs2.Windows
{
    public partial class ProdsInSaleWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        List<SaleProd> saleProds;
        List<ProductsInSaleViewModels> products;

        public ProdsInSaleWindow(Kurs2RudenkoContext kurs2RudenkoContext, int currenSaleId)
        {
            _context = kurs2RudenkoContext;
            saleProds = _context.SaleProds
                .Where(x => x.SaleId == currenSaleId)
                .ToList();
            products = new List<ProductsInSaleViewModels>();

            foreach (SaleProd prod in saleProds)
                products.Add(new ProductsInSaleViewModels(prod.ProductId, _context));

            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductList.ItemsSource = products;
        }
    }
}
