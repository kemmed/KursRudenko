using Kurs2.Database;
using Kurs2.Models;
using Kurs2.Models.ViewModels;
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
    
    public partial class ProdsInOrderWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        List<OrderProd> orderProds;
        List<ProductsInOrderViewModels> products;

        public ProdsInOrderWindow(Kurs2RudenkoContext kurs2RudenkoContext, int currentOrderId)
        {
            _context = kurs2RudenkoContext;
            orderProds=_context.OrderProds
                .Where(x=>x.OrderId==currentOrderId)
                .ToList();
            products = new List<ProductsInOrderViewModels>();   

            foreach (OrderProd prod in orderProds)
                products.Add(new ProductsInOrderViewModels(prod.ProductId, _context));
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductList.ItemsSource = products;
        }
    }
}
