using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xallet.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timestamp = (DateTime)value;
            return timestamp.ToString("MM/dd/yyyy @ HH:mm UTC");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
