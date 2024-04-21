using Kurs2.Database;
using Kurs2.Models;
using Kurs2.Models.ViewModels;
using Kurs2.Windows;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kurs2
{
    public partial class NewSaleWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        List<ProductAssortment> products;
        List<SaleProd> saleProds;
        Sale newSale;
        List<OrderSaleViewModel> saleViewModels;
        bool closeApp = true;

        public NewSaleWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
            summTB.Text = "0";

            saleProds = new List<SaleProd>();
            saleViewModels = new List<OrderSaleViewModel>();

            if (App.currentUser.Role == 3)
            {
                saleMI.Visibility = Visibility.Collapsed;
                userMI.Visibility = Visibility.Collapsed;
                otherMI.Visibility = Visibility.Collapsed;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_context.Orders != null && _context.Orders.Any())
                SaleIDTB.Text = "№ " + (_context.Orders.Max(x => x.OrderId) + 1).ToString();
            else
                SaleIDTB.Text = "№ 1";
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (closeApp)
                Environment.Exit(0);
        }

        private void Menu_clkMain(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            Close();
            MainWindow mainWindow = new MainWindow(_context);
            mainWindow.Show();
        }

        private void Menu_clkOrder(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            Close();
            AllOrdersWindow allOrders = new AllOrdersWindow(_context);
            allOrders.Show();
        }

        private void Menu_clkSale(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            Close();
            AllSalesWindow allSales = new AllSalesWindow(_context);
            allSales.Show();
        }
        private void Menu_clkUser(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            Close();
            UserWindow userSales = new UserWindow(_context);
            userSales.Show();
        }
        private void Menu_clkStock(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            Close();
            StockWindow stock = new StockWindow(_context);
            stock.Show();
        }
        private void Menu_clkAttrib(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            Close();
            AttributeWindow attribWindow = new AttributeWindow(_context);
            attribWindow.Show();
        }
        private void Menu_clkType(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            Close();
            TypeWindow typeSales = new TypeWindow(_context);
            typeSales.Show();
        }
        private void Menu_clkBrand(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            Close();
            BrandWindow brandSales = new BrandWindow(_context);
            brandSales.Show();
        }
        private void Button_clkClear(object sender, RoutedEventArgs e)
        {
            saleViewModels.Clear();
            saleProds.Clear();
            ProductList.ItemsSource = null;
            ProductList.ItemsSource = saleViewModels;
        }
        private void Refresh()
        {
            saleViewModels.Clear();
            for (int i = 0; i < saleProds.Count; i++)
            {
                OrderSaleViewModel saleVM = new OrderSaleViewModel(saleProds[i].ProductId, _context, saleProds[i].Count);
                saleViewModels.Add(saleVM);
            }
            ProductList.ItemsSource = null;
            ProductList.ItemsSource = saleViewModels;

        }
        private void Button_clkAdd(object sender, RoutedEventArgs e)
        {
            if (productIDTB.Text != "")
            {
                int productId = int.Parse(productIDTB.Text);

                if (!_context.ProductAssortments.Any(p => p.ProdAssortId == productId))
                {
                    MessageBox.Show("Товар с указанным ID не существует.", "Ошибка.", MessageBoxButton.OK);
                    return;
                }

                if (saleProds.Any(op => op.ProductId == productId))
                {
                    MessageBox.Show("Товар уже был добавлен в продажу.", "Предупреждение.", MessageBoxButton.OK);
                    return;
                }
                int countProdOnStock = _context.Stocks.FirstOrDefault
                        (y => y.StockId == _context.ProductAttribs.FirstOrDefault
                            (x => x.ProdAssortId == productId).StockId).Count;
                if (int.Parse(countTB.Text) > countProdOnStock)
                {
                    MessageBox.Show($"Вы указали большее количество единиц товара, чем на складе. Товара на складе {countProdOnStock} шт.", "Предупреждение.", MessageBoxButton.OK); ;
                    return;
                }

                if (newSale == null)
                {
                    newSale = new Sale();
                    newSale.SaleDate = DateTime.Now;
                }

                SaleProd productSale= new SaleProd();
                productSale.ProductId = productId;
                productSale.Count = int.Parse(countTB.Text);
                saleProds.Add(productSale);
                Refresh();

                summTB.Text = (decimal.Parse(summTB.Text) + saleViewModels.FirstOrDefault
                    (x => x.Id == productId).price * saleViewModels.FirstOrDefault (x => x.Id == productId).count).ToString();
            }
            else
                MessageBox.Show("Введите ID товара для добавления.", "Ошибка.", MessageBoxButton.OK);

            productIDTB.Text = "";
            countTB.Text = "1";
        }

        private void Button_clkPlus(object sender, RoutedEventArgs e)
        {
            countTB.Text = (int.Parse(countTB.Text) + 1).ToString();
        }

        private void Button_clkMinus(object sender, RoutedEventArgs e)
        {
            if (int.Parse(countTB.Text) != 1)
                countTB.Text = (int.Parse(countTB.Text) - 1).ToString();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) && e.Text != ".")
                e.Handled = true;
            else if (e.Text == "." && ((sender as TextBox).Text.Contains(".") || (sender as TextBox).Text.Length == 0))
                    e.Handled = true;
        }

        private void Button_clkPlaceOrder(object sender, RoutedEventArgs e)
        {
            if (saleProds.Count == 0)
            {
                MessageBox.Show("Для оформления продажи, добавте хотябы 1 товар.", "Предупреждение.", MessageBoxButton.OK);
                return;
            }

            _context.Sales.Add(newSale);

            _context.SaveChanges();
            foreach (SaleProd prod in saleProds)
            {
                prod.SaleId = newSale.SaleId;
                _context.SaleProds.Add(prod);
            }
            _context.SaveChanges();

            foreach (OrderSaleViewModel prod in saleViewModels)
                _context.Stocks.FirstOrDefault(y => y.StockId == _context.ProductAttribs.FirstOrDefault
                (x => x.ProdAssortId == prod.Id).StockId).Count -= prod.count;

            _context.SaveChanges();

            MessageBox.Show("Продажа успешно оформлена.", "", MessageBoxButton.OK);
            closeApp = false;
            Close();

            MainWindow mainWindow = new MainWindow(_context);
            mainWindow.Show();
        }
    }
}