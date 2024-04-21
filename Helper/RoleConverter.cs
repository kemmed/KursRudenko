using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Kurs2.Helper
{
    internal class RoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int role)
            {
                switch (role)
                {
                    case 1: return "Администратор";
                    case 2: return "Кассир";
                    default: return "Менеджер по снабжению";
                }
            }
            return "Пользователь";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
