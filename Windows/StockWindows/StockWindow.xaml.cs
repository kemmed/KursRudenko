using Kurs2.Database;
using Kurs2.Models;
using Kurs2.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Kurs2.Windows
{
    public partial class StockWindow : Window
    {
        List<ProductAttrib> attribs;
        List<StockViewModel> stockVMList;
        List<PlaceViewModel> stocksName;
        List<Stock> stocks;

        List<ComboBox> places;
        List<TextBox> count;

        private readonly Kurs2RudenkoContext _context;
        bool closeApp = true;
        public StockWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            InitializeComponent();
            stockVMList = new List<StockViewModel>();

            _context = kurs2RudenkoContext;        
            attribs = _context.ProductAttribs
                .Include(x => x.ProdAssort)
                .Include(x => x.Attribute)
                .ToList();

            // Группировка атрибутов по их товару
            var groupedAttribs = attribs.GroupBy(attr => attr.ProdAssort);

            foreach (var group in groupedAttribs)
            {
                var productName = group.Key.ProdAssortName;
                var attributes = string.Join(" ", group.Select(attr => 
                                                                $"{attr.AttributeValue.ToLower()} {attr.Attribute.AttributeValueType}"));

                stockVMList.Add(new StockViewModel(group.First(), $"{productName} {attributes}"));
            }

            stocks = _context.Stocks.ToList();
            stocksName = _context.Stocks.Select(x => new PlaceViewModel(x)).ToList();
            places = new List<ComboBox>();
            count = new List<TextBox>();

            foreach (var group in groupedAttribs)
            {
                var attrib = group.First();

                ComboBox placeCB = new ComboBox();
                if (stocksName.Any(x => x.stock.StockId == attrib.StockId))
                    placeCB.SelectedItem = stocksName.FirstOrDefault(x => x.stock.StockId == attrib.StockId);
                placeCB.ItemsSource = stocksName;
                placeCB.DisplayMemberPath = "place";
                placeCB.Tag = attrib;
                placeCB.Height = 20;

                TextBox countTB = new TextBox();
                if (stocksName.Any(x => x.stock.StockId == attrib.StockId))
                    countTB.Text = stocksName.FirstOrDefault(x => x.stock.StockId == attrib.StockId).stock.Count.ToString();
                    countTB.Width = 30;
                    countTB.Height = 20;
                    countTB.HorizontalAlignment = HorizontalAlignment.Center;
                    countTB.PreviewTextInput += TextBox_PreviewTextInput;
                    countTB.Tag = placeCB;
                
                places.Add(placeCB);
                count.Add(countTB);
            }
            Refresh();
        }
        private void Refresh()
        {
            foreach (ComboBox cb in places)
            {
                if (!PlaceCBoxes.Children.Contains(cb))
                    PlaceCBoxes.Children.Add(cb);
            }
            foreach (TextBox tb in count)
            {
                if (!CountBoxes.Children.Contains(tb))
                    CountBoxes.Children.Add(tb);
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) || e.Text == " ")
                e.Handled = true;
        }
        private void Window_Loaded(object sender, EventArgs e)
        {
            ListAttribs.ItemsSource = stockVMList
                .GroupBy(x => x.name)
                .Distinct();
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

        private void Button_clkAdd(object sender, RoutedEventArgs e)
        {
            NewStockPlaceWindow newPlase = new NewStockPlaceWindow(_context);
            newPlase.ShowDialog();
        }

        private void Button_clkSave(object sender, RoutedEventArgs e)
        {
            for(int i=0; i<places.Count;i++)
            {
                if (places[i].SelectedIndex!=-1 
                    && places[i].SelectedIndex != places.Count-1 
                    && i + 1 < places.Count 
                    && places[i].SelectedIndex == places[i + 1].SelectedIndex)
                {
                    MessageBox.Show("Место хранения товара не может повторяться.", "Ошибка", MessageBoxButton.OK);
                    return;
                }
            }
            foreach (var c in count)
            {
                if ((c.Tag as ComboBox).SelectedIndex != -1)
                {
                    ((c.Tag as ComboBox).Tag as ProductAttrib).Stock = ((c.Tag as ComboBox).SelectedItem as PlaceViewModel).stock;

                    if (c.Text.Replace(" ", "") == "")
                        ((c.Tag as ComboBox).Tag as ProductAttrib).Stock.Count = 0;
                    else
                        ((c.Tag as ComboBox).Tag as ProductAttrib).Stock.Count = int.Parse(c.Text.Replace(" ", ""));
                }

                else if (c.Text.Replace(" ", "") != "")
                {
                    MessageBox.Show("Для сохранения кол-ва товара, необходимо указать место на складе.", "Ошибка", MessageBoxButton.OK);
                    return;
                }
            }
            _context.SaveChanges();
        }
        private void TextChanged_Search(object sender, TextChangedEventArgs e)
        {
            string searchText = (sender as TextBox).Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(ListAttribs.ItemsSource);

            if (!string.IsNullOrEmpty(searchText))
                view.Filter = item => ((item as StockViewModel)?.name.ToLower().Contains(searchText) ?? false);
            else
                view.Filter = null;
        }


    }
}
