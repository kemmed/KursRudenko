
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
using Kurs2.Database;
using Kurs2.Helper;
using Kurs2.Models;

namespace Kurs2
{
    public partial class AuthorizationWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        public AuthorizationWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            InitializeComponent();
            _context = kurs2RudenkoContext;
        }

        private void Button_SignIn(object sender, RoutedEventArgs e)
        {
            List<User> userList = _context.Users.ToList();
            string log = login.Text;
            string pass = UserProcessings.GetPasswordHash(password.Password);
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].Login!=log || userList[i].Password != pass)
                    massage.Visibility = Visibility.Visible;
                else
                {
                    App.currentUser = userList[i];
                    massage.Visibility= Visibility.Hidden;
                    this.Hide();
                    MainWindow mainWindow = new MainWindow(_context);
                    mainWindow.Show();
                    break;
                }
            }

        }
    }
}
