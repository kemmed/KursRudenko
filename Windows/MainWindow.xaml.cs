using Kurs2.Database;
using Kurs2.Models.ViewModels;
using Kurs2.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Kurs2
{
    public partial class MainWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        bool closeApp = true;

        public MainWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
            if(App.currentUser.Role == 2)
            {
                orderMI.Visibility = Visibility.Collapsed;
                userMI.Visibility = Visibility.Collapsed;
                otherMI.Visibility = Visibility.Collapsed;
                deleteMI.Visibility = Visibility.Collapsed;
                newOrderBTN.Visibility = Visibility.Collapsed;
                newProdBTN.Visibility = Visibility.Collapsed;
            }
            else if(App.currentUser.Role == 3)
            {
                saleMI.Visibility = Visibility.Collapsed;
                userMI.Visibility = Visibility.Collapsed;
                otherMI.Visibility = Visibility.Collapsed;
                deleteMI.Visibility = Visibility.Collapsed;
                newSaleBTN.Visibility = Visibility.Collapsed;
                newProdBTN.Visibility = Visibility.Collapsed;
                newOrderBTN.HorizontalAlignment = HorizontalAlignment.Left;
            }
        }
        private void Refresh()
        {
            var ListOfAttribsWithStocks = _context.ProductAttribs
                .Include(x => x.Stock).Select(x =>
                    new { x.ProdAssortId, Count = (x.Stock != null ? x.Stock.Count : 0) })
                    .ToList();
            var products = _context.ProductAssortments
                .Include(x => x.Type)
                .Include(x => x.Вrand)
                .ToList();
            var productsView = products.Select(Product =>
            {
                return new ProductsViewModel(Product, ListOfAttribsWithStocks.FirstOrDefault(y => y.ProdAssortId == Product.ProdAssortId)?.Count);
            })
            .ToList();

            ProductList.ItemsSource = productsView;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            Refresh();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (closeApp)
                Environment.Exit(0);
        }

        private void Menu_clkMain(object sender, RoutedEventArgs e)
        {

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

        private void Button_clkSale(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            Close();
            NewSaleWindow newSale = new NewSaleWindow(_context);
            newSale.Show();
        }

        private void Button_clkOrder(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            Close();
            NewOrderWindow newOrder = new NewOrderWindow(_context);
            newOrder.Show();
        }
        private void Menu_clkBrand(object sender, RoutedEventArgs e)
        {
            closeApp = false; 
            Close();
            BrandWindow brandSales = new BrandWindow(_context);
            brandSales.Show();
        }

        private void Button_clkNew(object sender, RoutedEventArgs e)
        {
            NewProductWindow newProd = new NewProductWindow(_context);
            newProd.Show();
        }
        private void Menu_clkEditAttrib(object sender, RoutedEventArgs e)
        {
            ProductAttributeWindow Attrib = new ProductAttributeWindow(_context, (ProductList.SelectedItem as ProductsViewModel).Product);
            Attrib.Show();
            PopUpWin.Visibility = Visibility.Hidden;
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

        private void Menu_clkInfoEdit(object sender, RoutedEventArgs e)
        {
            ProductInfoWindow infoProd = new ProductInfoWindow(((ProductsViewModel)ProductList.SelectedItem).Product,_context);
            infoProd.Show();
            PopUpWin.Visibility = Visibility.Hidden;
        }
        private void Menu_clkDelete(object sender, RoutedEventArgs e)
        {
            PopUpWin.Visibility = Visibility.Hidden;
            var deleteProduct = (ProductList.SelectedItem as ProductsViewModel).Product;
            var result = MessageBox.Show("Вы ходите удалить товар?", "Подтверждение удаления", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (deleteProduct != null)
                {
                    _context.ProductAssortments.Remove(deleteProduct);
                    _context.SaveChanges();
                }
            }
            Refresh();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PopUpWin.Visibility = Visibility.Hidden;
        }
        private void TextChanged_Search(object sender, TextChangedEventArgs e)
        {
            string searchText = (sender as TextBox).Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(ProductList.ItemsSource);

            if (!string.IsNullOrEmpty(searchText))
                view.Filter = item => ((item as ProductsViewModel)?.Product.ProdAssortName.ToLower().Contains(searchText) ?? false);
            else
                view.Filter = null;
        }

        private void ToggleSortDirection(string sortBy)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ProductList.ItemsSource);

            if (view.SortDescriptions.Count > 0 && view.SortDescriptions[0].PropertyName == sortBy)
            {
                // Изменение направления сортировки
                ListSortDirection newDirection = view.SortDescriptions[0].Direction == 
                    ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(sortBy, newDirection));
            }
            else
            {
                // Добавление сортировки по возрастанию
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(sortBy, ListSortDirection.Ascending));
            }
        }
        private void CategoryColumn_clk(object sender, RoutedEventArgs e)
        {
            ToggleSortDirection("Product.Type.TypeName");
        }

        private void BrandColumn_clk(object sender, RoutedEventArgs e)
        {
            ToggleSortDirection("Product.Brand.BrandName");
        }

        private void CountColumn_clk(object sender, RoutedEventArgs e)
        {
            ToggleSortDirection("Count");
        }
    }
}