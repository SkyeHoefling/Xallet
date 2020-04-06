using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xallet.Converters
{
    public class SatoshiToBitcoinConverter : IValueConverter
    {
        private const double TokenFactor = 100000000;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (long)value / TokenFactor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (long)value * TokenFactor;
        }
    }
}
