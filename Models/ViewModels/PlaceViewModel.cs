using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs2.Models.ViewModels
{
    internal class PlaceViewModel
    {
        public Stock stock { get; set; }
        public string place { get; set; } = "";

        public PlaceViewModel() { }
        public PlaceViewModel(Stock stock)
        {
            this.stock = stock;
            this.place = stock.Place.Split(',')[0] + 'Ш' + stock.Place.Split(',')[1] + 'П';
        }
    }
}
