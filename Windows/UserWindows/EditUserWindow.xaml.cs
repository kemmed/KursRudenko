using Kurs2.Database;
using Kurs2.Helper;
using Kurs2.Models;
using Microsoft.VisualBasic.Logging;
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
    public partial class EditUserWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        List<User> users;
        User editUser;
        public EditUserWindow(Kurs2RudenkoContext kurs2RudenkoContext, User editUser)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
            this.editUser = editUser;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            users = _context.Users.ToList();
            TbLogin.Text= editUser.Login;
            RoleList.SelectedIndex = editUser.Role - 1; 
        }
        private void Button_clkClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text == "")
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK);
                return;
            }
            editUser.Login = TbLogin.Text;
            editUser.Role = RoleList.SelectedIndex+1;

            var userList = _context.Users.ToList();
            bool flag = true;
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].Login == TbLogin.Text && TbLogin.Text!= editUser.Login)
                {
                    flag = false;
                    MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка", MessageBoxButton.OK);
                    break;
                }

            }
            if (flag)
            {
                _context.Users.Update(editUser);
                _context.SaveChanges();
                MessageBox.Show("Информация о пользователе успешно изменена.", "", MessageBoxButton.OK);
                Close();
            }
        }
    }
}
