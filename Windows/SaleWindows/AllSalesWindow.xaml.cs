using Kurs2.Database;
using Kurs2.Models.ViewModels;
using Kurs2.Models;
using Kurs2.Windows;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;
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
    public partial class AllSalesWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        bool closeApp = true;
        List<AllSalesViewModel> saleViewModels;
        List<Sale> sales;

        public AllSalesWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
            sales = _context.Sales.ToList();
            saleViewModels = new List<AllSalesViewModel>();
            foreach (var sale in sales)
                saleViewModels.Add(new AllSalesViewModel(sale.SaleId, _context));

            if (App.currentUser.Role == 3)
            {
                saleMI.Visibility = Visibility.Collapsed;
                userMI.Visibility = Visibility.Collapsed;
                otherMI.Visibility = Visibility.Collapsed;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SalesLw.ItemsSource = saleViewModels;
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
        private void Button_clkCalendar(object sender, RoutedEventArgs e)
        {
            if (Calendar.Visibility == Visibility.Hidden)
                Calendar.Visibility = Visibility.Visible;
            else if (Calendar.Visibility == Visibility.Visible)
                Calendar.Visibility = Visibility.Hidden;
        }
        private void ProductList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PopUpWin.Visibility = Visibility.Hidden;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PopUpWin.Visibility = Visibility.Hidden;
        }
        private void ListViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            PopUpWin.Margin = new Thickness(Mouse.GetPosition(this).X, Mouse.GetPosition(this).Y, 0, 0);
            PopUpWin.HorizontalAlignment = HorizontalAlignment.Left;
            Debug.WriteLine(Mouse.GetPosition(this).X);
            PopUpWin.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.OriginalSource is ListViewItem))
                PopUpWin.Visibility = Visibility.Hidden;
        }

        private void TextChanged_Search(object sender, TextChangedEventArgs e)
        {
            string searchText = (sender as TextBox).Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(SalesLw.ItemsSource);

            if (!string.IsNullOrEmpty(searchText))
                view.Filter = item => ((AllSalesViewModel)item).Id.ToString().Contains(searchText);
            else
                view.Filter = null;
        }

        private void Button_clkOrderInfo(object sender, RoutedEventArgs e)
        {
            ProdsInSaleWindow prodsInSaleWindow = new ProdsInSaleWindow(_context, (SalesLw.SelectedItem as AllSalesViewModel).Id);
            prodsInSaleWindow.Show();
            PopUpWin.Visibility = Visibility.Hidden;
        }

        private void SelectDate(object sender, SelectionChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(SalesLw.ItemsSource);
            view.Filter = item => ((AllSalesViewModel)item).date.Year == Calendar.SelectedDate.Value.Year
            && ((AllSalesViewModel)item).date.Month == Calendar.SelectedDate.Value.Month
            && ((AllSalesViewModel)item).date.Day == Calendar.SelectedDate.Value.Day;
        }
    }
}