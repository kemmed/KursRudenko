using Kurs2.Database;
using Kurs2.Models;
using Kurs2.Windows;
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

namespace Kurs2
{
    public partial class UserWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        List<User> users;
        bool closeApp = true;

        public UserWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            users = _context.Users.ToList();
            UserList.ItemsSource = users;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            users = _context.Users.ToList();
            UserList.ItemsSource = users;
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

        private void Button_clkNew(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUser = new NewUserWindow(_context);
            newUser.Show();
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
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PopUpWin.Visibility = Visibility.Hidden;
        }
        private void TextChanged_Search(object sender, TextChangedEventArgs e)
        {
            string searchText = (sender as TextBox).Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(UserList.ItemsSource);

            if (!string.IsNullOrEmpty(searchText))
                view.Filter = item => ((User)item).Login.ToLower().Contains(searchText);
            else
                view.Filter = null;
        }
        private void Button_clkDelete(object sender, RoutedEventArgs e)
        {
            PopUpWin.Visibility = Visibility.Hidden;
            User deleteUser = (User)UserList.SelectedItem;
            if (App.currentUser.Login == deleteUser.Login)
            {
                MessageBox.Show($"Вы не можете удалить свою учетную запись.", "Ошибка", MessageBoxButton.OK);
                return;
            }
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить пользователя {deleteUser.Login}?", "Подтверждение удаления", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _context.Users.Remove(deleteUser);
                _context.SaveChanges();
                users = _context.Users.ToList();
                UserList.ItemsSource = users;
            }
        }
        private void Button_clkEdit(object sender, RoutedEventArgs e)
        {
            PopUpWin.Visibility = Visibility.Hidden;
            User editUser = (User)UserList.SelectedItem;
            EditUserWindow editUserWin = new EditUserWindow(_context, editUser);
            editUserWin.Show();
        }
        private void Button_clkEditPass(object sender, RoutedEventArgs e)
        {
            User editUser = (User)UserList.SelectedItem;
            PopUpWin.Visibility = Visibility.Hidden;
            EditPasswordWindow editPassWin = new EditPasswordWindow(_context, editUser);
            editPassWin.Show();
        }

    }
}
