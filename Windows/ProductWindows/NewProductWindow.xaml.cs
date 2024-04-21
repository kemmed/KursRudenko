using Kurs2.Database;
using Kurs2.Helper;
using Kurs2.Models;
using Kurs2.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class NewProductWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;
        List<TypeProd> types;
        List<Brand> brands;
        public NewProductWindow(Kurs2RudenkoContext kurs2RudenkoContext)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            types = _context.TypeProds.ToList();
            cbType.ItemsSource = types;
            brands = _context.Brands.ToList();
            cbBrand.ItemsSource = brands;
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
                TextBox textBox = sender as TextBox;
                string text = textBox.Text.Insert(textBox.CaretIndex, e.Text);
                string[] parts = text.Split(',');

                if (parts.Length > 2 || (parts.Length == 2 && parts[1].Length > 2) || (parts.Length == 1 && parts[0].Length > 5))
                    e.Handled = true;
            }
        }
        private void Button_clkSave(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text == "" || PriceTB.Text == "")
            {
                MessageBox.Show("Заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK);
                return;
            }
            ProductAssortment product = new ProductAssortment();
            product.ProdAssortName = NameTB.Text;
            product.ProdAssortDescription = DescriptionTB.Text;
            product.ВrandId = (cbBrand.SelectedItem as Brand).BrandId;
            product.ProdBasePrice = decimal.Parse(PriceTB.Text);
            product.TypeId = (cbType.SelectedItem as TypeProd).TypeId;

            List<Brand> brands = _context.Brands.ToList();

            var productList = _context.ProductAssortments.ToList();
            bool flag = true;
            
            if (flag)
            {
                _context.ProductAssortments.Add(product);
                _context.SaveChanges();
                MessageBox.Show("Товар успешно добавлен.", "", MessageBoxButton.OK);
                Close();
            }
        }

    }
}
