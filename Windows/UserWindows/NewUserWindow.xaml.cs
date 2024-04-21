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
using System.ComponentModel;
using Microsoft.VisualBasic.Logging;
using System.Windows.Forms;
using Kurs2.Helper;
using MessageBox = System.Windows.MessageBox;

namespace Kurs2
{
    public partial class NewUserWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        List<User> users;
        public NewUserWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            users = _context.Users.ToList();
        }

        private void Button_clkClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text == "" || TbPassword.Password == "")
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK);
                return;
            }
            User user = new User();
            user.Login = TbLogin.Text;
            user.Password = UserProcessings.GetPasswordHash(TbPassword.Password);
            user.Role = RoleList.SelectedIndex + 1;

            
            var userList = _context.Users.ToList();
            bool flag = true;
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].Login == user.Login)
                {
                    flag=false;
                    MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка", MessageBoxButton.OK);
                    break;
                }
                
            }
            if (flag)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                MessageBox.Show("Пользователь успешно добавлен.", "", MessageBoxButton.OK);
                Close();
            }
        }
    }
}
