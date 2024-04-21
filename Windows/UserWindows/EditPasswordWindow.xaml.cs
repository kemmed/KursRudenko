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

namespace Kurs2.Windows
{
    public partial class EditPasswordWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        User editPassUser;
        public EditPasswordWindow(Kurs2RudenkoContext kurs2RudenkoContext, User editPassUser)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
            this.editPassUser = editPassUser;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (TbPass.Password == "")
            {
                System.Windows.MessageBox.Show("Придумайте пароль.", "Ошибка", MessageBoxButton.OK);
                return;
            }
            editPassUser.Password = UserProcessings.GetPasswordHash(TbPass.Password);
            _context.SaveChanges();
            System.Windows.MessageBox.Show("Пароль успешно обновлен.", "", MessageBoxButton.OK);
            Close();
        }
        private void Button_clkClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
