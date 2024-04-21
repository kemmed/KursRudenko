using Kurs2.Database;
using Kurs2.Models;
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
    public partial class NewStockPlaceWindow : Window
    {
        Stock stockList;
        private readonly Kurs2RudenkoContext _context;
        public NewStockPlaceWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
        }

        private void Button_clkClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (ShelfTb.Text == "" || WardrobeTb.Text == "" || ShelfTb.Text == "0" || WardrobeTb.Text == "0")
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK);
                return;
            }
            Stock stock = new Stock();
            stock.Place = WardrobeTb.Text.Replace(" ", "") + ',' + ShelfTb.Text.Replace(" ", "");
            stock.Count = 0;

            var stockList = _context.Stocks.ToList();
            for (int i = 0; i < stockList.Count; i++)
                if (stockList[i].Place == stock.Place)
                {
                    MessageBox.Show("Место с такой комбинацией шкафа и полки уже существует.", "Ошибка", MessageBoxButton.OK);
                    return;
                }

            _context.Stocks.Add(stock);
            _context.SaveChanges();
            MessageBox.Show("Место успешно добавлено.", "", MessageBoxButton.OK);
            Close();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) || e.Text == " ")
                e.Handled = true;
        }
    }
}

