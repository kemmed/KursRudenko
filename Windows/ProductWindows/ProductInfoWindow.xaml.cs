using Kurs2.Database;
using Kurs2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kurs2
{
    public partial class ProductInfoWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        private ProductAssortment product;
      
        public ProductInfoWindow(ProductAssortment product, Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            this.product = product;

            InitializeComponent();

            DataContext = product;
            CbType.ItemsSource = _context.TypeProds.ToList();
            CbBrand.ItemsSource = _context.Brands.ToList();

            if(App.currentUser.Role!=1)
            {
                saveBTN.Visibility=Visibility.Collapsed;
                closeBTN.Visibility=Visibility.Collapsed;

                ProductName.IsReadOnly = true;
                Price.IsReadOnly = true;
                Description.IsReadOnly = true;

                CbType.IsEnabled = false;
                CbBrand.IsEnabled = false;
            }
        }
        private void Button_clkSave(object sender, RoutedEventArgs e)
        {
            if (Price.Text.Replace(" ","") == "")
                product.ProdBasePrice = 0;

            _context.SaveChanges();
            Close();
        }

        private void Button_clkClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) && e.Text != ",")
                e.Handled = true;
            else
            {
                System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
                string text = textBox.Text.Insert(textBox.CaretIndex, e.Text);
                string[] parts = text.Split(',');

                if (parts.Length > 2 || (parts.Length == 2 && parts[1].Length > 2) || (parts.Length == 1 && parts[0].Length > 6))
                    e.Handled = true;
            }
        }
    }
}
