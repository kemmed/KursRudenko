using Kurs2.Database;
using Kurs2.Models;
using Kurs2.Models.ViewModels;
using Kurs2.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    public partial class ProductAttributeWindow : Window
    {
        private readonly Kurs2RudenkoContext _context;

        List<AttributeProd> currentProductAttributes;
        List<ProductAttrib> tmpProdAttribs; 

        List<AttributeProd> allAvailableAttributes;

        List<TextBox> values;
        List<TextBox> prices;
        List<TextBlock> types;
        ProductAssortment prod;
        int? stockID = null;
        public ProductAttributeWindow(Kurs2RudenkoContext kurs2RudenkoContext, ProductAssortment prod)
        {
            _context = kurs2RudenkoContext;
            InitializeComponent();

            values = new List<TextBox>();
            prices = new List<TextBox>();
            types = new List<TextBlock>();
            this.prod = prod;

            if (App.currentUser.Role != 1)
            {
                addBTN.Visibility= Visibility.Collapsed;
                deleteBTN.Visibility= Visibility.Collapsed;
                saveBTN.Visibility= Visibility.Collapsed;
                closeBTN.Visibility= Visibility.Collapsed;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            tmpProdAttribs = _context.ProductAttribs
                .Include(x => x.Attribute)
                .Where(x=>x.ProdAssortId==prod.ProdAssortId)
                .ToList();

            currentProductAttributes = tmpProdAttribs
                .Select(x => x.Attribute)
                .ToList();

            prodAttribs.ItemsSource = currentProductAttributes;

            foreach (ProductAttrib attrib in tmpProdAttribs)
            {
                if (attrib.StockId != null)
                    stockID = attrib.StockId;
            }

            allAvailableAttributes = _context.AttributeProds.ToList().Except(currentProductAttributes).ToList();          
            allAttribs.ItemsSource = allAvailableAttributes;

            foreach (AttributeProd attributeProd in currentProductAttributes)
            {
                TextBox textBoxV = new TextBox();
                textBoxV.Text = tmpProdAttribs.FirstOrDefault(x => x.AttributeId == attributeProd.AttributeId).AttributeValue;
                textBoxV.Height = 20;

                TextBox textBoxP = new TextBox();
                textBoxP.Text = tmpProdAttribs.FirstOrDefault(x => x.AttributeId == attributeProd.AttributeId).PriceCoeff.ToString();
                textBoxP.Height = 20;
                textBoxP.PreviewTextInput += TextBox_PreviewTextInput;

                TextBlock valueType = new TextBlock();
                valueType.Height = 20;
                valueType.Text = attributeProd.AttributeValueType;

                values.Add(textBoxV);
                prices.Add(textBoxP);
                types.Add(valueType);
            }
            Refresh();
            
        }
        private void Button_clkClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_clkAddAttrib(object sender, RoutedEventArgs e)
        {
            if (allAttribs.SelectedItem != null)
            {
                currentProductAttributes.Add(allAttribs.SelectedItem as AttributeProd);
                allAvailableAttributes.Remove(allAttribs.SelectedItem as AttributeProd);

                prodAttribs.Items.Refresh();
                allAttribs.Items.Refresh();

                TextBox textBoxV = new TextBox();
                textBoxV.Height = 20;

                TextBox textBoxP = new TextBox();
                textBoxP.Text = "1";
                textBoxP.Height = 20;
                textBoxP.PreviewTextInput += TextBox_PreviewTextInput;


                TextBlock valueType = new TextBlock();
                valueType.Height = 20;
                foreach (AttributeProd attributeProd in currentProductAttributes)
                    valueType.Text = attributeProd.AttributeValueType;

                values.Add(textBoxV);
                prices.Add(textBoxP);
                types.Add(valueType);

                Refresh();
            }
        }

        private void Refresh()
        {
            foreach (TextBox tb in values)
            {
                if (!ValueBoxes.Children.Contains(tb))
                    ValueBoxes.Children.Add(tb);
            }
            foreach (TextBox tb in prices)
            {
                if (!PriceBoxes.Children.Contains(tb))
                    PriceBoxes.Children.Add(tb);
            }
            foreach (TextBlock l in types)
            {
                if (!ValueTypeBoxes.Children.Contains(l))
                    ValueTypeBoxes.Children.Add(l);
            }
            if (App.currentUser.Role != 1)
            {
                for (int i = 0; i < prices.Count; i++)
                    prices[i].IsReadOnly = true;

                for (int i = 0; i < values.Count; i++)
                    values[i].IsReadOnly = true;
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) && e.Text != ",")
                e.Handled = true;

            else if (e.Text == "," && ((sender as TextBox).Text.Contains(",") || (sender as TextBox).Text.Length == 0))
                    e.Handled = true;
        }

        private void Button_clkAddDelete(object sender, RoutedEventArgs e)
        {
            if (prodAttribs.SelectedItem != null)
            {
                currentProductAttributes.Remove(prodAttribs.SelectedItem as AttributeProd);
                allAvailableAttributes.Add(prodAttribs.SelectedItem as AttributeProd);

                prodAttribs.Items.Refresh();
                allAttribs.Items.Refresh();


                ValueBoxes.Children.RemoveAt(prodAttribs.SelectedIndex + 1);
                PriceBoxes.Children.RemoveAt(prodAttribs.SelectedIndex + 1);
                ValueTypeBoxes.Children.RemoveAt(prodAttribs.SelectedIndex + 1);

                values.RemoveAt(prodAttribs.SelectedIndex + 1);
                prices.RemoveAt(prodAttribs.SelectedIndex + 1);
                types.RemoveAt(prodAttribs.SelectedIndex + 1);
            }
        }
        private void Button_clkSave(object sender, RoutedEventArgs e)
        {
            _context.ProductAttribs.RemoveRange(_context.ProductAttribs.Where(x => x.ProdAssortId == prod.ProdAssortId));

            foreach (TextBox v in values)
            {
                if (v.Text == "")
                {
                    MessageBox.Show("Заполните все поля значений атрибутов.", "Ошибка", MessageBoxButton.OK);
                    return;
                }
            }
            foreach (AttributeProd prodAttribs in currentProductAttributes)
            {
                ProductAttrib attribs = new ProductAttrib();
                attribs.ProdAssortId = prod.ProdAssortId;
                attribs.AttributeId = prodAttribs.AttributeId;
                attribs.AttributeValue = values[currentProductAttributes.IndexOf(prodAttribs)].Text;

                if (prices[currentProductAttributes.IndexOf(prodAttribs)].Text == "")
                    attribs.PriceCoeff = 1;
                else
                    attribs.PriceCoeff = double.Parse(prices[currentProductAttributes.IndexOf(prodAttribs)].Text);

                if(stockID!=null)
                    attribs.StockId = stockID;

                _context.ProductAttribs.Add(attribs);
            }
            _context.SaveChanges();
            MessageBox.Show("Все изменения сохранены успешно.", "", MessageBoxButton.OK);
        }
    }
}
