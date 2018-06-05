using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace GoogleScraper.Converters
{
    public class StringAndIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strVal = value.ToString();

            if (string.IsNullOrEmpty(strVal))
                return 0;

            else
                return int.Parse(strVal);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : value.ToString();
        }
    }
}
