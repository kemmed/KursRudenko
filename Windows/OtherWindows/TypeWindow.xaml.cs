using Kurs2.Database;
using Microsoft.EntityFrameworkCore;
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
using Kurs2.Models;
using System.Diagnostics;
using Kurs2.Windows;
using System.ComponentModel;

namespace Kurs2
{
    public partial class TypeWindow : Window
        
    {
        private readonly Kurs2RudenkoContext _context;
        List<TypeProd> types;
        bool closeApp = true;

        public TypeWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            types = _context.TypeProds.ToList();
            TypesDG.ItemsSource = types;
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
        private void Button_clkSave(object sender, RoutedEventArgs e)
        {
            List<TypeProd> types = new List<TypeProd>();

            foreach (var attr in TypesDG.Items)
                if (attr is TypeProd)
                    types.Add(attr as TypeProd);

            foreach (var adding in types
                .Where(x => _context.Entry(x).State == EntityState.Detached))
                _context.TypeProds.Add(adding);
            _context.SaveChanges();
            MessageBox.Show("Изменения сохранены успешно.", "", MessageBoxButton.OK);
        }

        private void TypesDG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && TypesDG.SelectedItem is TypeProd)
            {
                _context.TypeProds.Remove(TypesDG.SelectedItem as TypeProd);
                _context.SaveChanges();
            }
        }
        private void TextChanged_Search(object sender, TextChangedEventArgs e)
        {
            string searchText = (sender as TextBox).Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(TypesDG.ItemsSource);

            if (!string.IsNullOrEmpty(searchText))
                view.Filter = item => ((TypeProd)item).TypeName.ToLower().Contains(searchText);
            else
                view.Filter = null;
        }
    }
}
