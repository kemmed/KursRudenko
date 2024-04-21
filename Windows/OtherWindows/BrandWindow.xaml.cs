using Kurs2.Database;
using Kurs2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace Kurs2.Windows
{
    public partial class BrandWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        List<Brand> brands;
        bool closeApp = true;

        public BrandWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            brands = _context.Brands.ToList();
            BrandsDG.ItemsSource = brands;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            brands = _context.Brands.ToList();
            BrandsDG.ItemsSource = brands;
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

        }
        private void Button_clkSave(object sender, RoutedEventArgs e)
        {
            List<Brand> brands = new List<Brand>();

            foreach (var attr in BrandsDG.Items)
                if (attr is Brand)
                    brands.Add(attr as Brand);

            foreach (var adding in brands.Where(x => _context.Entry(x).State == EntityState.Detached))
                _context.Brands.Add(adding);

            _context.SaveChanges();
            MessageBox.Show("Изменения сохранены успешно.", "", MessageBoxButton.OK);
        }

        private void BrandsDG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && BrandsDG.SelectedItem is Brand)
            {
                _context.Brands.Remove(BrandsDG.SelectedItem as Brand);
                _context.SaveChanges();
            }
        }
        private void TextChanged_Search(object sender, TextChangedEventArgs e)
        {
            string searchText = (sender as TextBox).Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(BrandsDG.ItemsSource);

            if (!string.IsNullOrEmpty(searchText))
                view.Filter = item => ((Brand)item).BrandName.ToLower().Contains(searchText);
            else
                view.Filter = null;
        }
    }
}
